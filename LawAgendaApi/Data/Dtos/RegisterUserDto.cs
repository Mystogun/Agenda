using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [ModelBinder(Name = "name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [ModelBinder(Name = "username")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [ModelBinder(Name = "password")]
        [MaxLength(32), MinLength(8)]
        public string Password { get; set; }

        [ModelBinder(Name = "phone_number")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [ModelBinder(Name = "phone_number_2")]
        [MaxLength(50)]
        public string PhoneNumber2 { get; set; }

        [ModelBinder(Name = "avatar")] public IFormFile? Picture { get; set; }
    }
}