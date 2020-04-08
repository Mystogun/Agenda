using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawAgendaApi.Data.Entities
{
    public class A1CaseType
    {
        [Key] public short Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsDeleted { get; set; }

        [MaxLength(255)] public string Type { get; set; }
        [MaxLength(10)] public string Color { get; set; }
    }
}