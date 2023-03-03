namespace bbqueue.Domain.Models
{
    public enum TicketState { Created, InProcess, Returned, Reopened, Closed }

    internal sealed class Ticket
    {
        public long Id { get; set; }

        public int Number { get; set; }

        public string PublicNumber { get; set; } = default!;

        public TicketState State { get; set; }

        public DateTime Created { get; set; }

        public DateTime Closed { get; set; }

    }
}
