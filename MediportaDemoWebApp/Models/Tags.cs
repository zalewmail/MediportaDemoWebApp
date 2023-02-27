using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MediportaDemoWebApp.Models
{
    public class Tags
    {
        [JsonPropertyName("items")]
        public List<Tag> Items { get; set; }
        
        [JsonPropertyName("has_more")]
        public bool isMore { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("count")]
        public int Count {get; set; }
        
        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

    }
}
