using bbqueue.Controllers;
using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Mapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Threading;

namespace bbqueue.Infrastructure.Services
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        public readonly IWindowRepository windowRepository;
        private readonly IHubContext<TicketsHub> ticketsHub;

        public TicketService(ITicketRepository ticketRepository, IWindowRepository windowRepository, IHubContext<TicketsHub> ticketsHub) 
        {
            this.ticketRepository = ticketRepository;
            this.windowRepository = windowRepository;
            this.ticketsHub = ticketsHub;
        }

        public async Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {
            var ticket = await ticketRepository.CreateTicketAsync(targetId, cancellationToken);
            await RefreshOnlineQueueAsync();
            return ticket;
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
            await RefreshOnlineQueueAsync();
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
            await RefreshOnlineQueueAsync();
        }

        public async Task CloseTicket(long ticketId, long userId, CancellationToken cancellationToken)
        {
            var ticket = await ticketRepository.GetTicketByIdAsync(ticketId, cancellationToken);
            if(ticket == null)
            {
                throw new ApiException(ExceptionEvents.TicketNotFound);
            }

            var ticketOperationList = await ticketRepository.GetTicketOperationByTicket(ticketId, cancellationToken);
            var userInLastTicketOperation = ticketOperationList.OrderByDescending(to=>to.Processed).Select(to=>to.EmployeeId).FirstOrDefault();
            if(userInLastTicketOperation != null && userInLastTicketOperation != userId) 
            {
                throw new ApiException(ExceptionEvents.TicketNotRelatedToThisEmployee);
            }
            ticket.State = TicketState.Closed;
            ticket.Closed = DateTime.UtcNow;
            var window = await windowRepository.GetWindowByEmployeeId(userId, cancellationToken);
            var operation = new TicketOperation()
            {
                TicketId = ticketId,
                EmployeeId = userId,
                Processed = DateTime.UtcNow,
                TargetId = ticket.TargetId,
                WindowId = window.Id,
                State = TicketState.Closed
            };
            await ticketRepository.AddTicketOperation(operation, ticket, cancellationToken);
            await RefreshOnlineQueueAsync();
        }

        public async Task<string> GetTicketsForOnlineQueueAsync()
        {
            var result = await ticketRepository.LoadTicketsFromDbAsync(false, CancellationToken.None);
            TicketListDto ticketListDtos = new TicketListDto();
            ticketListDtos.Tickets = new();
            foreach (var ticket in result)
            {
                ticketListDtos.Tickets.Add(ticket.FromModelToDto());
            }
            return JsonConvert.SerializeObject(ticketListDtos);
        }

        private async Task RefreshOnlineQueueAsync()
        {
            var messageR = await GetTicketsForOnlineQueueAsync();
            await ticketsHub.Clients.All.SendAsync(messageR);
        }
    }
}
