using System.Text.Json.Serialization;

namespace Models
{
    public class TicketOperation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ticket")]
        public Ticket Ticket { get; set; }

        [JsonPropertyName("window")]
        public Window Window { get; set; }

        [JsonPropertyName("employee")]
        public Employee Employee { get; set; }

        [JsonPropertyName("state")]
        public TicketState State { get; set; }

        [JsonPropertyName("processed")]
        public DateTime Processed { get; set; }
    }
}
