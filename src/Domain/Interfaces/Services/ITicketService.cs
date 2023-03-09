using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface ITicketService
    {
        public Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken);
        public Task ChangeTicketTarget(long ticketNumber, long targetCode, CancellationToken cancellationToken);
        public Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);
        public Task TakeTicketToWork(Ticket ticket, long windowId, CancellationToken cancellationToken);

    }
}
