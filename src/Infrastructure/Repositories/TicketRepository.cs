using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class TicketRepository:ITicketRepository
    {
        private readonly QueueContext queueContext;
        public TicketRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public async Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, char prefix, CancellationToken cancellationToken)
        {
            queueContext.TicketEntity.Add(ticketEntity);
            await SaveLastTicketNumberAsync(ticketEntity.Number, prefix, cancellationToken);
            await queueContext.SaveChangesAsync(cancellationToken);
            return ticketEntity.Id;
        }

        public async Task<Ticket?> GetTicketByIdAsync(long ticketId, CancellationToken cancellationToken)
        {
            var ticket = await queueContext.TicketEntity.SingleOrDefaultAsync(t => t.Id == ticketId,cancellationToken);
            return ticket == null ? default! : ticket.FromEntityToModel();
        }

        public async Task UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            var ticket = await queueContext.TicketEntity.FirstOrDefaultAsync(te=>te.Id==ticketEntity.Id, cancellationToken);
            if (ticket == null)
                throw new Exception("Не найдено талона для обновления");//Тут нужен норм эксепшн

            ticket.State=ticketEntity.State;
            ticket.Closed=ticketEntity.Closed;
            ticket.TargetId=ticketEntity.TargetId; 
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)//true грузим обработанные талоны false необработанные талоны
        {
            var state = loadOnlyProcessedTickets ? TicketState.Closed : TicketState.Created;
            return queueContext.TicketEntity
                .Where(te => te.State == state)
                .Select(te=>te.FromEntityToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task SaveLastTicketNumberAsync(int number, char prefix, CancellationToken cancellationToken)
        {
            var ticketAmount = await queueContext.TicketAmountEntity.SingleOrDefaultAsync(tae=>tae.Prefix==prefix && tae.Number == number-1, cancellationToken);
            if(ticketAmount == null)
            {
                await CreateTicketAmountRecordAsync(prefix, cancellationToken);
                return;
            }
            ticketAmount.Number = number;
            await queueContext.SaveChangesAsync(cancellationToken);
        }

        public Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId, CancellationToken cancellationToken)
        {

            var ticketOperations = from ticketOperation in queueContext.TicketOperationEntity
                                   join windowTarget in queueContext.WindowTargetEntity
                                   on ticketOperation.TargetId equals windowTarget.TargetId
                                   where windowTarget.WindowId == windowId
                                   select ticketOperation.FromEntityToModel();

            return ticketOperations.ToListAsync(cancellationToken);
        }

        public Task<List<TicketOperation>> GetTicketOperationByTicket(long ticketId, CancellationToken cancellationToken)
        {
            return queueContext.TicketOperationEntity.Where(to => to.TicketId == ticketId).Select(to=>to.FromEntityToModel()).ToListAsync(cancellationToken);
        }

        public Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken)
        {
            queueContext.TicketOperationEntity.Add(ticketOperationEntity);
            return queueContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TicketAmount> GetTicketAmountAsync(long targetId, CancellationToken cancellationToken)
        {
            var target = await queueContext.TargetEntity.SingleOrDefaultAsync(t => t.Id == targetId);
            if (target == null)
                return default!;
            var ticketAmount = await queueContext
                        .TicketAmountEntity
                        .SingleOrDefaultAsync(ta => ta.Prefix == target.Prefix, cancellationToken);
            if(ticketAmount == null)
            {
                ticketAmount = await CreateTicketAmountRecordAsync(target.Prefix, cancellationToken);
            }
            return ticketAmount.FromEntityToModel();
        }

        private async Task<TicketAmountEntity> CreateTicketAmountRecordAsync(char prefix, CancellationToken cancellationToken)
        {
            TicketAmountEntity ticketAmountEntity = new TicketAmountEntity()
            {
                Prefix = prefix,
                Number = 0
            };
            queueContext.TicketAmountEntity.Add(ticketAmountEntity);
            await queueContext.SaveChangesAsync(cancellationToken);
            return ticketAmountEntity;
        }

        public async Task<Ticket?> GetNextTicketAsync(long employeeId, CancellationToken cancellationToken)
        {
            var window = await WindowRelatedToEmployeeasync(employeeId,cancellationToken);
            if (window == null)
                throw new Exception("Невозможно взять в работу следующий талон. Пользователь не прикреплён к окну.");
            var query = await (from toe in queueContext.TicketOperationEntity
                        join
                        te in queueContext.TicketEntity
                        on toe.TicketId equals te.Id
                        join
                        wte in queueContext.WindowTargetEntity
                        on toe.TargetId equals wte.TargetId
                        where wte.WindowId == window.Id
                        && te.State == TicketState.Created || te.State == TicketState.Returned//вот тут посложнее логику надо сделать
                        orderby te.Created
                        select te).FirstOrDefaultAsync(cancellationToken);
            return query == null ? default! : query.FromEntityToModel();
        }

        public async Task<Ticket?> GetNextSpecificTicketAsync(long ticketId, long employeeId, CancellationToken cancellationToken)
        {
            var window = await WindowRelatedToEmployeeasync(employeeId, cancellationToken);
            if (window == null)
                throw new Exception("Невозможно взять в работу следующий талон. Пользователь не прикреплён к окну.");
            var ticket = await queueContext.TicketEntity.SingleOrDefaultAsync(te=>te.Id == ticketId, cancellationToken);
            return ticket == null? default! : ticket.FromEntityToModel();
        }


        private Task<WindowEntity?> WindowRelatedToEmployeeasync(long employeeId, CancellationToken cancellationToken)
        {
           return (from win in queueContext.WindowEntity
                                where win.EmployeeId == employeeId
                                select win).FirstOrDefaultAsync(cancellationToken);
        }

        public Task DeleteAllTicketsFromDBAsync(CancellationToken cancellationToken)
        {
            queueContext.RemoveRange(queueContext.TicketAmountEntity);
            queueContext.RemoveRange(queueContext.TicketOperationEntity);
            queueContext.RemoveRange(queueContext.TicketEntity);
            return queueContext.SaveChangesAsync(cancellationToken);
        }
    }
}
