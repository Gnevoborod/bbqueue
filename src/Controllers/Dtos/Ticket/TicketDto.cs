namespace bbqueue.Controllers.Dtos.Ticket
{
    public class TicketDto
    {
        public long Id { get; set; }

        public int Number { get; set; }

        public string PublicNumber { get; set; } = default!;
        public long TargetId { get; set; }
        public DateTime Created { get; set; }
    }
}
