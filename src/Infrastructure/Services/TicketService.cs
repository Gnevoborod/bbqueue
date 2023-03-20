﻿using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using System.Net.Sockets;
using System.Threading;

namespace bbqueue.Infrastructure.Services
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IQueueService queueService;
        public readonly IWindowRepository windowRepository;
        public TicketService(ITicketRepository ticketRepository, IQueueService queueService, IWindowRepository windowRepository) 
        {
            this.ticketRepository = ticketRepository;
            this.queueService = queueService;
            this.windowRepository = windowRepository;
        }

        public async Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {
            var ticketAmount = await ticketRepository.GetTicketAmountAsync(targetId, cancellationToken);
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
            int nextNumber = ++ticketAmount.Number;
            ticket.Number = nextNumber;
            ticket.PublicNumber = prefix + nextNumber.ToString();
            ticket.TargetId = targetId;
            ticket.Id = await ticketRepository.SaveTicketToDbAsync(ticket.FromModelToEntity(), prefix, cancellationToken);

            await ticketRepository.SaveTicketOperationToDbAsync(new TicketOperationEntity()
            {
                TicketId = ticket.Id,
                TargetId = targetId,
                State = TicketState.Created,
                Processed = DateTime.UtcNow

            }, cancellationToken); ;

            return ticket;
        }
        public async Task ChangeTicketTarget(long ticketId, long targetId, long employeeId, CancellationToken cancellationToken)
        {
            //сразу проверяем существует ли такой талон
            var ticket = await ticketRepository.GetTicketByIdAsync(ticketId, cancellationToken);
            if (ticket == null)
                throw new Exception("В базе не обнаружен талон");
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
            await ticketRepository.SaveTicketOperationToDbAsync(ticketOperation.FromModelToEntity(), cancellationToken);
            await ticketRepository.UpdateTicketInDbAsync(ticket.FromModelToEntity(), cancellationToken);
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
            await ticketRepository.SaveTicketOperationToDbAsync(ticketOperation.FromModelToEntity(), cancellationToken);
            await ticketRepository.UpdateTicketInDbAsync(ticket.FromModelToEntity(), cancellationToken);
        }
    }
}
