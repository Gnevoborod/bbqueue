using bbqueue.Domain.Interfaces.Repositories;
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

        public async Task AddTicketToQueueAsync(Ticket ticket)
        {
            await Task.Run(() => { Thread.Sleep(100); });
        }
        public async Task ReturnTicketToQueueAsync(Ticket ticket, CancellationToken cancellationToken) 
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }

        public async Task RemoveTicketAsync(Ticket ticket, CancellationToken cancellationToken) 
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }

        public async Task ClearQueueAsync(CancellationToken cancellationToken) 
        {
            await Task.Run(() => { Thread.Sleep(100); }); 
        }

        public async Task<Ticket> GetTicketNextTicketFromQueueAsync(long windowId, CancellationToken cancellationToken) 
        {
            await Task.Run(() => { Thread.Sleep(100); });
            return new();//тут нужно будет дёргать очередь
        }

        public async Task<Ticket> GetNextSpecificTicketFromQueueAsync(long ticketNumber, CancellationToken cancellationToken)
        { 
            await Task.Run(() => { Thread.Sleep(100); });
            return new();
        }

        public async  Task RestoreQueueAsync(CancellationToken cancellationToken)
        { 
            await Task.Run(() => { Thread.Sleep(100); });
        }


        private async Task<List<Ticket>> ListOfTicketsForSpecificWindowAsync(long windowId, CancellationToken cancellationToken)
        {
            var tickets = await GetListOfTicketsAsync(windowId, cancellationToken);
            /*lock (obj)
            {
                ticketListForWindow = (from ticketList in TicketList
                                       join ticket in tickets
                                       on ticketList.Id equals ticket
                                       select ticketList).ToList();
            }
            Пока перенёс логику из класса очереди, чтоб не забыть как оно было*/
            return new();
        }

        private async Task<List<long>> GetListOfTicketsAsync(long windowId, CancellationToken cancellationToken)
        {
            //переписать(?)
            var ticketOperations = await serviceProvider
                           .GetService<ITicketRepository>()!
                           .GetTicketOperationByWindowPlusTargetAsync(windowId, cancellationToken);
            List<long> tickets = new();
            foreach (var to in ticketOperations)
            {
                tickets.Add((long)to.TicketId!);
            }
            return tickets;
        }
    }
}
