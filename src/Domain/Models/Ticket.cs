namespace bbqueue.Domain.Models
{
    public enum TicketState { Created, InProcess, Reopened, Closed }

    internal sealed class Ticket
    {
        public long Id { get; set; }

        public int Number { get; set; }

        public TicketState State { get; set; }

        public DateTime Created { get; set; }

        public DateTime Closed { get; set; }
    }
}
