using bbqueue.Domain.Models;
using bbqueue.Database.Entities;

namespace bbqueue.Domain.Interfaces.Repositories
{
    internal interface ITicketRepository
    {
        public Task<bool> SaveTicketToDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        public Task<bool> UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);
        public Task<bool> SaveLastTicketNumberAsync(int number, string prefix, CancellationToken cancellationToken);
        public Task<int> GetLastTicketNumberAsync(string prefix, CancellationToken cancellationToken);
    }
}
