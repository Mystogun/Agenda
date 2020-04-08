using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawAgendaApi.Data.Entities
{
    public class User
    {
        [Key] public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte IsDeleted { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string PhoneNumber2 { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordAccess { get; set; }

        public short? TypeId { get; set; }
        public virtual A1UserType? Type { get; set; }

        public long? FileId { get; set; }
        public virtual File? Avatar { get; set; }
    }
}