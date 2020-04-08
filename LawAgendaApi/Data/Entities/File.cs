using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawAgendaApi.Data.Entities
{
    public class
        File
    {
        [Key] public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Guid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsDeleted { get; set; }

        [MaxLength(512)] public string Path { get; set; }
        [MaxLength(10)] public string Extension { get; set; }


        public short? TypeId { get; set; }
        public virtual A1FileType? Type { get; set; }
    }
}