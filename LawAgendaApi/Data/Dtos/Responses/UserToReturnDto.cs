using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Data.Dtos.Responses
{
    public class UserToReturnDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }

        public virtual UserTypeToReturnDto? Type { get; set; }

        public virtual FileToReturnDto? Avatar { get; set; }
    }
}