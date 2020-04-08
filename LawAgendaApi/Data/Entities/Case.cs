using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawAgendaApi.Data.Entities
{
    public class Case
    {
        [Key] public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsDeleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsPrivate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsClosed { get; set; }
        
        [MaxLength(255)]
        public string Number { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Defendant { get; set; }
        public string CourtHouse { get; set; }
        public string Judge { get; set; }
        public string EFN { get; set; }

        public DateTime? StartingDate { get; set; }
        public DateTime? NextDate { get; set; }
        public DateTime? NotificationDate { get; set; }

        public long? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public short? TypeId { get; set; }
        public virtual A1CaseType? Type { get; set; }
        
        public double? Price { get; set; }
    }
}