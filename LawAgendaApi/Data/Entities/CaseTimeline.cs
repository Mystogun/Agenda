using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawAgendaApi.Data.Entities
{
    public class CaseTimeline
    {
        [Key] public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsDeleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsPleading { get; set; }

        public DateTime? PleadingDate { get; set; }
        
        public string Notes { get; set; }

        public long? UserId { get; set; }
        public virtual User? User { get; set; }

        public long? CaseId { get; set; }
        public virtual Case? Case { get; set; }

        public long? FileId { get; set; }
        public virtual File? File { get; set; }
    }
}