namespace bbqueue.Domain.Models
{
    public enum TicketState { Created, InProcess, Returned, Reopened, Closed }

    public sealed class Ticket
    {
        public long Id { get; set; }

        public int Number { get; set; }

        public string PublicNumber { get; set; } = default!;

        public TicketState State { get; set; }

        public long TargetId { get; set; }
        public Target Target { get; set; } = default!;

        public DateTime Created { get; set; }

        public DateTime Closed { get; set; }

    }
}
