using System;
using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Data.Dtos.Responses
{
    public class CaseToReturnDto
    {
        public long Id { get; set; }

        public byte IsPrivate { get; set; }
        public byte IsClosed { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Defendant { get; set; }
        public string CourtHouse { get; set; }
        public string Judge { get; set; }
        public string EFN { get; set; }

        public DateTime? StartingDate { get; set; }
        public DateTime? NextDate { get; set; }
        public DateTime? NotificationDate { get; set; }

        public virtual CustomerToReturnDto? Customer { get; set; }

        public virtual CaseTypeToReturnDto? Type { get; set; }

        public double? Price { get; set; }
    }
}