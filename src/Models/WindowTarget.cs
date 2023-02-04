using System.Text.Json.Serialization;

namespace Models
{
    public class WindowTarget
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("window")]
        public Window Window { get; set; }

        [JsonPropertyName("target")]
        public Target Target { get; set; }
    }
}
