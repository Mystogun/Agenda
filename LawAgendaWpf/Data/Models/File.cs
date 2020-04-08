using System;
using Newtonsoft.Json;

namespace LawAgendaWpf.Data.Models
{
    public class File
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Path { get; set; }

        [JsonProperty("extension", NullValueHandling = NullValueHandling.Ignore)]
        public string Extension { get; set; }

    }
}