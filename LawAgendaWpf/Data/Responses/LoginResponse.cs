using LawAgendaWpf.Data.Models;
using Newtonsoft.Json;

namespace LawAgendaWpf.Data.Responses
{
    public partial class LoginResponse
    {
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
