using System.Text.Json.Serialization;

namespace Models
{
    public enum TicketState { Created, InProcess, Reopened, Closed }
    public class Ticket
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("state")]
        public TicketState State { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("closed")]
        public DateTime Closed { get; set; }
    }
}
