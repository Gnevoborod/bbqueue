namespace bbqueue.Domain.Models
{
    public sealed class TicketOperation
    {
        public long Id { get; set; }

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;

        public long? TargetId { get; set; }
        public Target? Target { get; set; }

        public long? WindowId { get; set; }

        public Window? Window { get; set; }

        public long? EmployeeId { get; set; }

        public TicketState State { get; set; }

        public DateTime Processed { get; set; }

    }
}
