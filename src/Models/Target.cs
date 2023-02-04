using System.Text.Json.Serialization;

namespace Models
{
    public class Target
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("groupLink")]
        public Group? GroupLink { get; set; }
    }
}

