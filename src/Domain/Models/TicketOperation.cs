namespace bbqueue.Domain.Models
{
    internal sealed class TicketOperation
    {
        public long Id { get; set; }

        public long TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;

        public long? WindowId { get; set; }

        public Window? Window { get; set; }

        public long? EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public TicketState State { get; set; }

        public DateTime Processed { get; set; }
    }
}
