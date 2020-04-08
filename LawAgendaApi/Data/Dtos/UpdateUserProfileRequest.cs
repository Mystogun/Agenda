using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class UpdateUserProfileRequest
    {
        [ModelBinder(Name = "id")] public long Id { get; set; }
        [ModelBinder(Name = "phone_number")] public string PhoneNumber { get; set; }
        [ModelBinder(Name = "phone_number_2")] public string PhoneNumber2 { get; set; }
        [ModelBinder(Name = "avatar")] public IFormFile? Avatar { get; set; }
    }
}