using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using System.Threading;

namespace bbqueue.Infrastructure.Services
{
    internal class TicketService: ITicketService
    {
        IServiceProvider serviceProvider;
        public TicketService(IServiceProvider _serviceProvider) 
        {
            serviceProvider = _serviceProvider;
        }

        public async Task<bool> SaveTicketAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<ITicketRepository>()?.SaveTicketToDbAsync(ticketEntity, cancellationToken)!;
        }
        public async Task<bool> ChangeTicketTarget(long ticketNumber, long targetCode, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return new();
        }
        public async Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<ITicketRepository>()?.LoadTicketsFromDbAsync(loadOnlyProcessedTickets, cancellationToken)!;
        }
    }
}
