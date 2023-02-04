using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ExternalSystemIdentity")]
        public string ExternalSystemIdentity { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Active")]
        public bool Active { get; set; }
    }
}