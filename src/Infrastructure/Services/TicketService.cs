using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using System.Threading;

namespace bbqueue.Infrastructure.Services
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        //TODELETE serviceProvider
        IServiceProvider serviceProvider;
        public TicketService(IServiceProvider _serviceProvider, ITicketRepository ticketRepository) 
        {
            serviceProvider = _serviceProvider;
            this.ticketRepository = ticketRepository;
        }

        public async Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {
            var ticketAmount = await ticketRepository.GetTicketAmountAsync(targetId)!;
            if(ticketAmount == null )
            {
                throw new Exception("Для указанной цели отсутствует префикс");//пока так, потом надо будет припилить собственный экспешн
            }
            Ticket ticket = new()
            {
                State = TicketState.Created,
                Created = DateTime.UtcNow
            };
            char prefix = ticketAmount.Prefix;
            int nextNumber = (int)serviceProvider.GetService<Queue>()?.AddTicket(ticket, prefix)!;
            ticket.Number = nextNumber;
            ticket.PublicNumber = prefix + nextNumber.ToString();
            ticket.Id = await ticketRepository.SaveTicketToDbAsync(ticket.FromModelToEntity()!, cancellationToken)!;
            return ticket;
        }
        public async Task ChangeTicketTarget(long ticketNumber, long targetCode, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
        }
        public async Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)
        {
            return await ticketRepository.LoadTicketsFromDbAsync(loadOnlyProcessedTickets, cancellationToken)!;
        }

        public bool TakeTicketToWork(Ticket ticket, long windowId)
        {
            ticket.State = TicketState.InProcess;
            var ticketOperation = ticketRepository.GetTicketOperationByTicket(ticket.Id);
            if (ticketOperation == null)
                return false;
            ticketOperation.WindowId = windowId;
            var result = ticketRepository.UpdateTicketOperationToDbAsync(ticketOperation.FromModelToEntity()!);
            return result == null ? false : result.Result;
        }
    }
}
