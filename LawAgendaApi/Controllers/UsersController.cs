using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LawAgendaApi.Data.Dtos;
using LawAgendaApi.Data.Dtos.Responses;
using LawAgendaApi.Data.Queries.Search;
using LawAgendaApi.Helpers;
using LawAgendaApi.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        // GET : api/v1/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = (await _userRepo.GetAll()).ToList();

            var returnUsers = _mapper.Map<IList<UserToReturnDto>>(users);

            return Ok(new
            {
                Users = returnUsers
            });
        }

        // GET : api/v1/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute(Name = "id")] long userId)
        {
            var user = await _userRepo.GetUserById(userId);

            var returnUser = _mapper.Map<UserToReturnDto>(user);

            return Ok(new
            {
                User = returnUser
            });
        }

        // GET : api/v1/users/search
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery(Name = "query")] string query,
            [FromQuery(Name = "filter")] string filter)
        {
            filter = filter.ConvertToPascalCase();

            if (!Enum.TryParse(filter, out UserSearchQueryType queryType))
            {
                return BadRequest();
            }

            var searchQuery = new SearchQuery<UserSearchQueryType>
            {
                Query = query,
                Filter = queryType
            };

            var usersFromDb = await _userRepo.Search(searchQuery, true);

            var users = _mapper.Map<IEnumerable<UserToReturnDto>>(usersFromDb);
            return Ok(new
            {
                Users = users
            });
        }

        // GET : api/v1/users/find
        [HttpGet("find")]
        public async Task<IActionResult> FindUserContaining([FromQuery(Name = "query")] string query)
        {
            var searchQuery = new SearchQuery<UserSearchQueryType>
            {
                Query = query,
            };

            var usersFromDb = await _userRepo.Search(searchQuery, false);

            var users = _mapper.Map<IEnumerable<UserToReturnDto>>(usersFromDb);
            return Ok(new
            {
                Users = users
            });
        }

        // PUT : api/v1/users
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserProfileRequest updateRequest)
        {
            var userFromDb = await _userRepo.GetUserById(updateRequest.Id);
            if (userFromDb == null)
            {
                return BadRequest();
            }

            HttpContext.Request.Headers.TryGetValue("Authorization", out var bearer);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.ToString().Split(" ")[1]);

            var idFromToken = token.Claims.ToList().FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (string.IsNullOrEmpty(idFromToken) || string.IsNullOrWhiteSpace(idFromToken) ||
                updateRequest.Id != Convert.ToInt64(idFromToken))
            {
                return Unauthorized();
            }

            var avatar = userFromDb.Avatar;
            await Tools.SaveFile(avatar.Id, updateRequest.Avatar);
            userFromDb.UpdatedAt = DateTime.UtcNow.AddHours(3);
            userFromDb.PhoneNumber = updateRequest.PhoneNumber;
            userFromDb.PhoneNumber2 = updateRequest.PhoneNumber2;

            var result = await _userRepo.EditProfile(userFromDb);
            switch (result.Message)
            {
                case "Not Found":
                    return BadRequest();
                case "Could not Save Changes":
                    return StatusCode(501, new
                    {
                        Errors = new[]
                        {
                            "Could not Save Changes, Try Again Later"
                        }
                    });
                case "Success":
                    var returnUser = _mapper.Map<UserToReturnDto>(result.Entity);
                    return Ok(new
                    {
                        User = returnUser
                    });
                default:
                    return StatusCode(500, new
                    {
                        Errors = new[]
                        {
                            "Unknown Error, Try Again Later"
                        }
                    });
            }
        }
    }
}