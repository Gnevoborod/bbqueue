using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ticket_amount")]
    public class TicketAmount
    {
        [Column("ticket_amount_id")]
        public int Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [MaxLength(1)]
        [Column("prefix")]
        public string Prefix { get; set; } = null!;
    }
}
