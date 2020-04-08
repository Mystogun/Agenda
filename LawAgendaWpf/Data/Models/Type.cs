using Newtonsoft.Json;

namespace LawAgendaWpf.Data.Models
{
    public class Type
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}