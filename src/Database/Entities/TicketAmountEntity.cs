using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bbqueue.Database.Entities
{
    [Table("ticket_amount")]
    public sealed class TicketAmountEntity
    {
        [Key, Column("ticket_amount_id")]
        public long Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("prefix")]
        public char Prefix { get; set; }
    }
}
