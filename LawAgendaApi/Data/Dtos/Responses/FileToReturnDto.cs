using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Data.Dtos.Responses
{
    public class FileToReturnDto
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}