using bbqueue.Domain.Models;
using bbqueue.Database.Entities;

namespace bbqueue.Domain.Interfaces.Repositories
{
    internal interface ITicketRepository
    {
        public Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        public Task<bool> UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);
        public Task<bool> SaveLastTicketNumberAsync(int number, string prefix, CancellationToken cancellationToken);
        public Task<int> GetLastTicketNumberAsync(string prefix, CancellationToken cancellationToken);
        public Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId);
        public Task<TicketOperation?> GetTicketOperationByTicketAsync(long ticketId);
        public TicketOperation? GetTicketOperationByTicket(long ticketId);
        public Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity);
        public Task<bool> UpdateTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity);
        public Task<TicketAmount> GetTicketAmountAsync(long targetId);
    }
}
