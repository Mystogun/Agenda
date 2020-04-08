using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LawAgendaApi.Data.Dtos;
using LawAgendaApi.Data.Dtos.Responses;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Repositories.AuthRepo;
using LawAgendaApi.Repositories.FileRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using File = LawAgendaApi.Data.Entities.File;

namespace LawAgendaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        private readonly IFileRepo _fileRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepo authRepo, IFileRepo fileRepo, IMapper mapper, IConfiguration configuration)
        {
            _authRepo = authRepo;
            _fileRepo = fileRepo;
            _mapper = mapper;
            _configuration = configuration;
        }

        // POST : api/v1/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterUserDto userDto)
        {

            var r = userDto.Name.Split();

            Console.WriteLine();
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var exists = await _authRepo.UserExists(userDto.Username);

            if (exists)
            {
                return Conflict("there is already a user with that username");
            }

            var mappedUser = _mapper.Map<User>(userDto);

            mappedUser.TypeId = 2;

            if (userDto.Picture != null)
            {
                var file = new File
                {
                    TypeId = 1,
                };

                file = await _fileRepo.CreateFile(file);

                file.Path = await SaveFile(file.Id, userDto.Picture);
                var sections = file.Path.Split('.');
                file.Extension = sections[^1];

                mappedUser.Avatar = file;
            }

            var user = _authRepo.Register(mappedUser, userDto.Password);

            var returnUser = _mapper.Map<UserToReturnDto>(user.Result);

            return Ok(new {User = returnUser});
        }

        // POST : api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var userFromRepo = await _authRepo.Login(userDto.Username, userDto.Password);
            if (userFromRepo == null)
            {
                return BadRequest();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
                new Claim(ClaimTypes.Role, userFromRepo.Type.Type), 
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Keys:TokenKey").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var user = _mapper.Map<UserToReturnDto>(userFromRepo);
            return Ok(new
            {
                User = user,
                Token = tokenHandler.WriteToken(token)
            });
        }


        #region Http Path Helpers

        private async Task<string> SaveFile(long fileId, IFormFile file)
        {
            var path = GetUploadingPath(fileId);
            var fileName = GetFileName(fileId, file.FileName);
            await using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var httpPath = GetHttpPath(path, fileName);
            return httpPath;
        }

        private string GetUploadingPath(long fileId)
        {
            var path = Path.Combine(@"C:\LawyersAgenda", "Avatars");

            var division = fileId / 10000;
            var multi = division * 10000;
            var summation = multi + 9999;
            var section1 = $"{multi:000000000000}";
            var section2 = $"{summation:000000000000}";

            var name = $"{section1}-{section2}";

            path = Path.Combine(path, name);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        private string GetFileName(long fileId, string fileName)
        {
            var sections = fileName.Split('.');

            var name = $"{fileId}.{sections[1]}";

            return name;
        }

        private string GetHttpPath(string path, string fileName)
        {
            var index = path.LastIndexOf("\\") + 1;
            var trimmed = path.Substring(index);
            var httpPath = $"http://agenda.e-iraq.net/files/avatars/{trimmed.Replace('\\', '/')}/{fileName}";
            return httpPath;
        }

        #endregion
    }
}