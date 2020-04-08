using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class CloseCaseByIdRequest
    {
        [ModelBinder(Name = "admin_id")] public long UserId { get; set; }
    }
}