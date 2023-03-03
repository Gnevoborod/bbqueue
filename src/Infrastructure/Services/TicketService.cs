using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
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

        public async Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {
            var provider = serviceProvider.GetService<ITicketRepository>();
            var ticketAmount = await provider?.GetTicketAmountAsync(targetId)!;

            Ticket ticket = new()
            {
                State = TicketState.Created,
                Created = DateTime.UtcNow
            };
            char prefix = ticketAmount.Prefix;
            int nextNumber = (int)serviceProvider.GetService<Queue>()?.AddTicket(ticket, prefix)!;
            ticket.Number = nextNumber;
            ticket.PublicNumber = prefix + nextNumber.ToString();
            ticket.Id = await provider?.SaveTicketToDbAsync(ticket.FromModelToEntity()!, cancellationToken)!;
            return ticket;
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

        public bool TakeTicketToWork(Ticket ticket, long windowId)
        {
            ticket.State = TicketState.InProcess;
            var provider = serviceProvider.GetService<ITicketRepository>();
            var ticketOperation = provider?.GetTicketOperationByTicket(ticket.Id);
            ticketOperation!.WindowId = windowId;
            var result = provider?.UpdateTicketOperationToDbAsync(ticketOperation.FromModelToEntity()!)!;
            return result.Result;
        }
    }
}
