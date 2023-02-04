using System.Text.Json.Serialization;

namespace Models
{
    public class TicketAmount
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }
    }
}
