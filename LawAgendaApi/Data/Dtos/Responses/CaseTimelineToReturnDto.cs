using System;
using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Data.Dtos.Responses
{
    public class CaseTimelineToReturnDto
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public byte IsPleading { get; set; }
        public DateTime? PleadingDate { get; set; }
        public virtual UserToReturnDto? User { get; set; }
        public long? FileId { get; set; }
        public virtual FileToReturnDto? File { get; set; }
    }
}