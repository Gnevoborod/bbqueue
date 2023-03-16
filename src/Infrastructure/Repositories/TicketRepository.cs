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
        public async Task UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            var ticket = await queueContext.TicketEntity.FirstOrDefaultAsync(te=>te.Id==ticketEntity.Id, cancellationToken);
            if (ticket == null)
                throw new Exception("Не найдено талона для обновления");//Тут нужен норм эксепшн

            ticket.State=ticketEntity.State;
            ticket.Closed=ticketEntity.Closed;
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

        public async Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId, CancellationToken cancellationToken)
        {

            var ticketOperations = from ticketOperation in queueContext.TicketOperationEntity
                                   join windowTarget in queueContext.WindowTargetEntity
                                   on ticketOperation.TargetId equals windowTarget.TargetId
                                   where windowTarget.WindowId == windowId
                                   select ticketOperation.FromEntityToModel();

            return await ticketOperations.ToListAsync(cancellationToken);
        }

        public async Task<TicketOperation> GetTicketOperationByTicketAsync(long ticketId, CancellationToken cancellationToken)
        {
            var ticketOperationEntity = await queueContext.TicketOperationEntity.FirstOrDefaultAsync(to => to.TicketId == ticketId, cancellationToken);
            return ticketOperationEntity == null? default! : ticketOperationEntity.FromEntityToModel();
        }

        public async Task<TicketOperation> GetTicketOperationByTicket(long ticketId, CancellationToken cancellationToken)
        {
            var ticketOperationEntity = await queueContext.TicketOperationEntity.FirstOrDefaultAsync(to => to.TicketId == ticketId, cancellationToken);
            return ticketOperationEntity == null ? default! : ticketOperationEntity.FromEntityToModel();
        }

        public async Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken)
        {
            queueContext.TicketOperationEntity.Add(ticketOperationEntity);
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken)
        {
            var ticketOperationEntityInDb = await queueContext.TicketOperationEntity.FirstOrDefaultAsync(toe=>toe.TicketId==ticketOperationEntity.TicketId);
            if (ticketOperationEntityInDb == null)
                throw new Exception("Не найдено записи по операции с талоном для обновления");//Тут нужен норм эксепшн

            ticketOperationEntityInDb.State = ticketOperationEntity.State;
            ticketOperationEntityInDb.WindowId = ticketOperationEntity.WindowId;
            ticketOperationEntityInDb.TargetId = ticketOperationEntity.TargetId;
            ticketOperationEntityInDb.EmployeeId = ticketOperationEntity.EmployeeId;
            ticketOperationEntityInDb.Updated = DateTime.UtcNow;
            await queueContext.SaveChangesAsync(cancellationToken);
            
            
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
            var window = await (from win in queueContext.WindowEntity
                                where win.EmployeeId == employeeId
                                select win).FirstOrDefaultAsync(cancellationToken);
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
    }
}
