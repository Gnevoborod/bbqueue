using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class TicketRepository:ITicketRepository
    {
        IServiceProvider serviceProvider;
        public TicketRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<bool> SaveTicketToDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return true;
        }
        public async Task<bool> UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return true;
        }
        public async Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)//true грузим обработанные талоны false необработанные талоны
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return new();
        }

        public async Task <bool> SaveLastTicketNumberAsync(int number, string prefix, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return true;
        }

        public async Task<int> GetLastTicketNumberAsync(string prefix, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return 0;
        }

    }
}
