using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class CustomerToCreateRequestDto
    {
        [ModelBinder(Name = "name")]public string Name { get; set; }
        [ModelBinder(Name = "username")]public string Username { get; set; }
        [ModelBinder(Name = "phone_number")]public string PhoneNumber { get; set; }
        [ModelBinder(Name = "phone_number_2")]public string PhoneNumber2 { get; set; }
    }
}