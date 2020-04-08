namespace LawAgendaApi.Data.Queries.Search
{
    public class SearchQuery<T>
    {
        public string Query { get; set; }
        public T Filter { get; set; }
    }
}