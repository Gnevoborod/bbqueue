using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface ITicketService
    {
        public Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken);
        public Task<bool> ChangeTicketTarget(long ticketNumber, long targetCode, CancellationToken cancellationToken);
        public Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);
        public bool TakeTicketToWork(Ticket ticket, long windowId);

    }
}
