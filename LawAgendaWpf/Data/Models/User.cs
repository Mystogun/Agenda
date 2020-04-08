using LawAgendaWpf.Data.Responses;
using Newtonsoft.Json;

namespace LawAgendaWpf.Data.Models
{
    public class User
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("phoneNumber2", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber2 { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public Type Type { get; set; }

        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public File Avatar { get; set; }

    }
}