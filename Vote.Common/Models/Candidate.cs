

namespace Vote.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Candidate

    {
        [JsonProperty("id")]
        public long Id { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public byte[] ImageArray { get; set; }

        [JsonProperty("imageFullPath")]
        public string ImageFullPath { get; set; }

    }
}
