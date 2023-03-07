using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using bbqueue.Domain.Models;
namespace bbqueue.Database.Entities
{
    [Table("ticket")]
    public sealed class TicketEntity
    {
        [Key, Column("ticket_id")]
        public long Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("public_number")]
        public string PublicNumber { get; set; } = default!;

        [Column("state")]
        public TicketState State { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("closed")]
        public DateTime Closed { get; set; }
    }
}
