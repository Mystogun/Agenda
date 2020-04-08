using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class CaseTimelineToCreateRequestDto
    {
        [ModelBinder(Name = "user_id")] public long UserId { get; set; }
        [ModelBinder(Name = "notes")] public string Notes { get; set; }
        [ModelBinder(Name = "files")] public IEnumerable<IFormFile>? Files { get; set; }
    }
}