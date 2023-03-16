using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bbqueue.Domain.Models;
namespace bbqueue.Database.Entities
{
    [Table("ticket_operation")]
    public sealed class TicketOperationEntity
    {
        [Key, Column("ticket_opearation_id")]
        public long Id { get; set; }

        [Column("ticket_id")]
        public long TicketId { get; set; }
        [ForeignKey(nameof(TicketId))]
        public TicketEntity Ticket { get; set; } = default!;

        [Column("target_id")]
        public long? TargetId { get; set; }
        [ForeignKey(nameof(TargetId))]
        internal TargetEntity? TargetEntity { get; set; }

        [Column("window_id")]
        public long? WindowId { get; set; }

        [ForeignKey(nameof(WindowId))]
        public WindowEntity? Window { get; set; }

        [Column("employee_id")]
        public long? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity? Employee { get; set; }

        [Column("state")]
        public TicketState State { get; set; }

        [Column("processed")]
        public DateTime Processed { get; set; }

        [Column("updated")]
        public DateTime? Updated { get; set; }
    }
}
