using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public enum TicketState { Created, InProcess, Reopened, Closed }
    [Table("ticket")]
    public class Ticket
    {
        [Column("ticket_id")]
        public int Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("state")]
        public TicketState State { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("closed")]
        public DateTime Closed { get; set; }
    }
}
