using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;

namespace bbqueue.Infrastructure.Services
{
    public class QueueService:IQueueService
    {
        IServiceProvider serviceProvider;

        public QueueService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task ReturnTicketToQueue(Ticket ticket, CancellationToken cancellationToken) 
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }

        public async Task RemoveTicket(Ticket ticket, CancellationToken cancellationToken) 
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }

        public async Task ClearQueue(CancellationToken cancellationToken) 
        {
            await Task.Run(() => { Thread.Sleep(100); }); 
        }

        public async Task<Ticket?> GetTicketNextTicketFromQueue(long windowId, CancellationToken cancellationToken) 
        {
            return await serviceProvider
                .GetService<Queue>()?
                .GetNextTicketFromQueueAsync(windowId, cancellationToken)!; // получаем талон
        }

        public async Task<Ticket> GetNextSpecificTicketFromQueue(long ticketNumber, CancellationToken cancellationToken)
        { 
            await Task.Run(() => { Thread.Sleep(100); });
            return new();
        }

        public async  Task RestoreQueue(CancellationToken cancellationToken)
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }
    }
}
