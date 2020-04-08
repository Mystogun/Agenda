using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class CustomerToUpdateRequestDto
    {
        [ModelBinder(Name = "id")] public long Id { get; set; }
        [ModelBinder(Name = "name")] public string Name { get; set; }
        [ModelBinder(Name = "username")] public string Username { get; set; }
        [ModelBinder(Name = "phone_number")] public string PhoneNumber { get; set; }
        [ModelBinder(Name = "phone_number_2")] public string PhoneNumber2 { get; set; }
    }
}