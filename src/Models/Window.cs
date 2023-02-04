using System;
using System.Text.Json.Serialization;

namespace Models
{
	public class Window
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("employee")]
        public Employee Employee { get; set; }
	}
}