using bbqueue.Domain.Models;
using bbqueue.Database.Entities;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        public Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, char prefix, CancellationToken cancellationToken);
        public Task UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);
        public Task SaveLastTicketNumberAsync(int number, char prefix, CancellationToken cancellationToken);
        public Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId, CancellationToken cancellationToken);
        public Task<TicketOperation> GetTicketOperationByTicketAsync(long ticketId, CancellationToken cancellationToken);
        public Task<TicketOperation> GetTicketOperationByTicket(long ticketId, CancellationToken cancellationToken);
        public Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken);
        public Task UpdateTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken);
        public Task<TicketAmount> GetTicketAmountAsync(long targetId, CancellationToken cancellationToken);
        public Task<Ticket?> GetNextTicketAsync(long windowId, CancellationToken cancellationToken);
    }
}
