using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ticket_operation")]
    public class TicketOperation
    {
        [Column("ticket_opearation_id")]
        public int Id { get; set; }

        [Column("ticket_id")]
        public Ticket Ticket { get; set; } = null!;

        [Column("window_id")]
        public Window? Window { get; set; }

        [Column("employee_id")]
        public Employee? Employee { get; set; } 

        [Column("state")]
        public TicketState State { get; set; }

        [Column("processed")]
        public DateTime Processed { get; set; }
    }
}
