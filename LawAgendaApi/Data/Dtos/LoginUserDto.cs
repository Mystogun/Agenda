using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class LoginUserDto
    {
        [Required]
        [ModelBinder(Name = "username")]
        public string Username { get; set; }

        [MaxLength(32), MinLength(8)]
        [ModelBinder(Name = "password")]
        public string Password { get; set; }
    }
}