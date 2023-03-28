using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Mapper;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Threading;

namespace bbqueue.Infrastructure.Services
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        public readonly IWindowRepository windowRepository;

        public TicketService(ITicketRepository ticketRepository, IWindowRepository windowRepository) 
        {
            this.ticketRepository = ticketRepository;
            this.windowRepository = windowRepository;
        }

        public Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {
            return ticketRepository.CreateTicketAsync(targetId, cancellationToken);
        }
        public async Task ChangeTicketTarget(long ticketId, long targetId, long employeeId, CancellationToken cancellationToken)
        {
            //сразу проверяем существует ли такой талон
            var ticket = await ticketRepository.GetTicketByIdAsync(ticketId, cancellationToken);
            if (ticket == null)
            {
                throw new ApiException(ExceptionEvents.TicketNotFound);
            }
            var ticketOperation = new TicketOperation();
            ticketOperation.State = TicketState.Returned;
            ticketOperation.TargetId = targetId;
            ticketOperation.Processed = DateTime.UtcNow;
            ticketOperation.EmployeeId = employeeId;
            ticketOperation.TicketId = ticketId;

            ticketOperation.WindowId = null;//очищаем окно так как сменили цель
            ticket.TargetId = targetId;
            ticket.State= TicketState.Returned;
            //обновляем данные в базе
            await ticketRepository.SaveTicketOperationToDbAsync(ticketOperation, cancellationToken);
            await ticketRepository.UpdateTicketInDbAsync(ticket, cancellationToken);
        }
        public Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)
        {
            return ticketRepository.LoadTicketsFromDbAsync(loadOnlyProcessedTickets, cancellationToken);
        }

        public async Task TakeTicketToWork(Ticket ticket, long employeeId, CancellationToken cancellationToken)
        {
            ticket.State = TicketState.InProcess;
            var ticketOperation = new TicketOperation();
            ticketOperation.EmployeeId = employeeId;
            var window = await windowRepository.GetWindowByEmployeeId(employeeId, cancellationToken);
            ticketOperation.WindowId = window.Id;
            ticketOperation.State= TicketState.InProcess;
            ticketOperation.Processed = DateTime.UtcNow;
            ticketOperation.TargetId = ticket.TargetId;
            ticketOperation.TicketId = ticket.Id;

            await ticketRepository.AddTicketOperation(ticketOperation, ticket, cancellationToken);

        }
    }
}
