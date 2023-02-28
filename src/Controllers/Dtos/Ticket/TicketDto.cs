using bbqueue.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace bbqueue.Controllers.Dtos.Ticket
{
    public class TicketDto
    {
        public long Id { get; set; }

        public int Number { get; set; }

        public DateTime Created { get; set; }
    }
}
