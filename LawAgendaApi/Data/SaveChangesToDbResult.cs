namespace LawAgendaApi.Data
{
    public class SaveChangesToDbResult<T>
    {
        public string Message { get; set; }
        public T Entity { get; set; }
    }
}