using System.Text.Json.Serialization;

namespace Models
{
    public class Group
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("groupLink")]
        public Group? GroupLink { get; set; }
    }
}
