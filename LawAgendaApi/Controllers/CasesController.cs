using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LawAgendaApi.Data.Dtos;
using LawAgendaApi.Data.Dtos.Responses;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;
using LawAgendaApi.Helpers;
using LawAgendaApi.Repositories;
using LawAgendaApi.Repositories.FileRepo;
using LawAgendaApi.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CasesController : ControllerBase
    {
        private readonly ICaseRepo _caseRepo;
        private readonly IUserRepo _userRepo;
        private readonly IFileRepo _fileRepo;
        private readonly IMapper _mapper;

        public CasesController(ICaseRepo caseRepo, IUserRepo userRepo, IMapper mapper, IFileRepo fileRepo)
        {
            _caseRepo = caseRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _fileRepo = fileRepo;
        }

        // GET : api/v1/cases
        [HttpGet]
        public async Task<IActionResult> GetPublicCases([FromQuery(Name = "created")] string created,
            [FromQuery(Name = "type_id")] short? typeId = null)
        {
            if (typeId != null)
            {
                var casesFromDb = await _caseRepo.GetCasesByType(typeId.Value);

                var casesByType = _mapper.Map<IEnumerable<CaseToReturnDto>>(casesFromDb);

                return Ok(new {Cases = casesByType});
            }

            try
            {
                if (string.IsNullOrEmpty(created) || string.IsNullOrWhiteSpace(created))
                {
                    var casesFromDb = await _caseRepo.GetPublicCases();

                    casesFromDb = casesFromDb.OrderBy(c => c.CreatedAt);

                    var cases = _mapper.Map<IEnumerable<CaseToReturnDto>>(casesFromDb);

                    return Ok(new {Cases = cases});
                }

                var createdAt = DateTime.Parse(created.Trim());

                var casesByCreationFromDb = await _caseRepo.GetPublicCases(createdAt);

                casesByCreationFromDb = casesByCreationFromDb.OrderBy(c => c.CreatedAt);

                var casesByCreation = _mapper.Map<IEnumerable<CaseToReturnDto>>(casesByCreationFromDb);

                return Ok(new {Cases = casesByCreation});
            }
            catch (FormatException e)
            {
                return BadRequest(new
                {
                    Errors = new[]
                    {
                        "Date Format is Not Recognized"
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Errors = new[]
                    {
                        e.Message,
                        "Unknown Error"
                    }
                });
            }
        }

        // GET : api/v1/cases/5
        [HttpGet("{id}", Name = "GetCaseById")]
        public async Task<IActionResult> GetCaseById([FromRoute(Name = "id")] long caseId)
        {
            if (caseId <= 0)
            {
                return BadRequest();
            }

            var caseFromDb = await _caseRepo.GetCaseById(caseId);

            var caseToReturn = _mapper.Map<CaseToReturnDto>(caseFromDb);

            return Ok(new {Case = caseToReturn});
        }

        // GET : api/v1/cases/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchForCase([FromQuery(Name = "query")] string query,
            [FromQuery(Name = "filter")] string filter, [FromQuery(Name = "operation")] string operation)
        {
            filter = filter.ConvertToPascalCase();

            operation = operation.ConvertToCamelCase();

            if (!Enum.TryParse(filter, out CaseSearchQueryType queryType))
            {
                return BadRequest();
            }

            var operationTryParseResult = Enum.TryParse(operation, out PriceOperationType operationType);

            var searchQuery = new SearchQuery<CaseSearchQueryType>
            {
                Query = query,
                Filter = queryType,
            };

            operationType = operationTryParseResult ? operationType : PriceOperationType.Unknown;

            var casesFromDb = await _caseRepo.Search(searchQuery, operationType, true);

            return Ok(new
            {
                Cases = casesFromDb
            });
        }

        // GET : api/v1/cases/find
        [HttpGet("find")]
        public async Task<IActionResult> FindCase([FromQuery(Name = "query")] string query)
        {
            var searchQuery = new SearchQuery<CaseSearchQueryType>
            {
                Query = query,
            };

            var casesFromDb = await _caseRepo.Search(searchQuery, PriceOperationType.Unknown, false);

            return Ok(new
            {
                Cases = casesFromDb
            });
        }

        // GET : api/v1/users/5/cases
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesForUser([FromRoute(Name = "id")] long userId,
            [FromQuery(Name = "page")] int? page = null, [FromQuery(Name = "page_size")] int? pageSize = null,
            [FromQuery(Name = "created")] DateTime? created = null, [FromQuery(Name = "next")] DateTime? next = null)
        {
            var userFromDb = await _userRepo.GetUserById(userId);

            if (userFromDb == null)
            {
                return BadRequest();
            }

            HttpContext.Request.Headers.TryGetValue("Authorization", out var bearer);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.ToString().Split(" ")[1]);

            var claims = token.Claims.ToList();

            var idFromToken = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (string.IsNullOrEmpty(idFromToken) || string.IsNullOrWhiteSpace(idFromToken) ||
                userId != Convert.ToInt64(idFromToken))
            {
                return Unauthorized();
            }

            var roleFromToken = claims.FirstOrDefault(c => c.Type == "role")?.Value;
            if (string.IsNullOrEmpty(roleFromToken) || string.IsNullOrWhiteSpace(roleFromToken))
            {
                if (userFromDb.Type.Type == roleFromToken)
                {
                    page ??= 1;
                    pageSize ??= 30;
                    var casesForAdmin = await _caseRepo.GetAllCasesForAdmin(page.Value, pageSize.Value);
                    return Ok(new
                    {
                        Cases = casesForAdmin
                    });
                }
            }

            var cases = await _caseRepo.GetCasesForUser(userId, created, next);

            return Ok(new {Cases = cases});
        }

        // POST : api/v1/cases/
        [HttpPost]
        public async Task<IActionResult> CreateCase([FromForm] CaseToCreateRequestDto caseToCreate)
        {
            var userFromRepo = await _userRepo.GetUserById(caseToCreate.AdminId);

            if (userFromRepo == null)
            {
                return BadRequest();
            }

            HttpContext.Request.Headers.TryGetValue("Authorization", out var bearer);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.ToString().Split(" ")[1]);

            var claims = token.Claims.ToList();
            var idFromToken = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            var roleFromToken = claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (string.IsNullOrEmpty(idFromToken) || string.IsNullOrWhiteSpace(idFromToken) ||
                caseToCreate.AdminId != Convert.ToInt64(idFromToken) || string.IsNullOrEmpty(roleFromToken) ||
                string.IsNullOrWhiteSpace(roleFromToken) || userFromRepo.Type.Id > 1)
            {
                return Unauthorized();
            }

            var lawyers = new List<User>();

            foreach (var id in caseToCreate.LawyersIds)
            {
                lawyers.Add(await _userRepo.GetUserById(id));
            }

            var newCase = _mapper.Map<Case>(caseToCreate);

            var newCaseResult = await _caseRepo.CreateCase(newCase, userFromRepo, lawyers);

            var caseToReturn = _mapper.Map<CaseToReturnDto>(newCaseResult.Entity);

            return CreatedAtAction("GetCaseById", new
            {
                newCaseResult.Entity.Id
            }, new
            {
                Case = caseToReturn
            });
        }

        // GET : api/v1/cases/5/timeline
        [HttpGet("{id}/timeline")]
        public async Task<IActionResult> GetTimelineByCaseId([FromRoute(Name = "id")] long caseId)
        {
            var timelineFromDb = await _caseRepo.GetCaseTimelineByCaseId(caseId);

            var timeline = _mapper.Map<IEnumerable<CaseTimelineToReturnDto>>(timelineFromDb);


            return Ok(new {Timeline = timeline});
        }

        // GET : api/v1/cases/5/timeline/1
        [HttpGet("{id}/timeline/{timelineId}", Name = "GetTimelineById")]
        public async Task<IActionResult> GetTimelineById([FromRoute(Name = "id")] long caseId,
            [FromRoute(Name = "timelineId")] long timelineId)
        {
            var timeline = await _caseRepo.GetCaseTimelineById(timelineId);

            return Ok(new {Timeline = timeline});
        }


        // POST : api/v1/cases/5/timeline
        [HttpPost("{id}/timeline")]
        public async Task<IActionResult> AddToCaseTimeline([FromRoute(Name = "id")] long caseId,
            [FromForm] CaseTimelineToCreateRequestDto caseTimelineToCreateDto)
        {
            var userFromRepo = await _userRepo.GetUserById(caseTimelineToCreateDto.UserId);

            if (userFromRepo == null)
            {
                return BadRequest();
            }

            HttpContext.Request.Headers.TryGetValue("Authorization", out var bearer);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.ToString().Split(" ")[1]);

            var claims = token.Claims.ToList();
            var idFromToken = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (string.IsNullOrEmpty(idFromToken) || string.IsNullOrWhiteSpace(idFromToken) ||
                caseTimelineToCreateDto.UserId != Convert.ToInt64(idFromToken))
            {
                return Unauthorized();
            }


            var newTimeline = _mapper.Map<CaseTimeline>(caseTimelineToCreateDto);

            newTimeline.CaseId = caseId;
            newTimeline.PleadingDate = DateTime.UtcNow.AddHours(3);

            var addResult = await _caseRepo.AddToCaseTimeline(newTimeline);

            var timeline = _mapper.Map<CaseTimelineToReturnDto>(addResult.Entity);

            switch (addResult.Message)
            {
                case "Success":
                    return Ok(new {Timeline = timeline});

                default:
                    return StatusCode(500);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CloseCaseById([FromRoute(Name = "id")] long caseId,
            [FromForm] CloseCaseByIdRequest closeRequest)
        {
            var userFromDb = await _userRepo.GetUserById(closeRequest.UserId);

            if (userFromDb == null)
            {
                return BadRequest();
            }

            HttpContext.Request.Headers.TryGetValue("Authorization", out var bearer);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.ToString().Split(" ")[1]);

            var claims = token.Claims.ToList();
            var idFromToken = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            var roleFromToken = claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (string.IsNullOrEmpty(idFromToken) || string.IsNullOrWhiteSpace(idFromToken) ||
                closeRequest.UserId != Convert.ToInt64(idFromToken) || string.IsNullOrEmpty(roleFromToken) ||
                string.IsNullOrWhiteSpace(roleFromToken) || userFromDb.Type.Type != roleFromToken)
            {
                return Unauthorized();
            }


            var closingResult = await _caseRepo.CloseCaseById(caseId);

            switch (closingResult.Message)
            {
                case "Success":
                    return NoContent();
                default:
                    return StatusCode(500);
            }
        }
    }
}