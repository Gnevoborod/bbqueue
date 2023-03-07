using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class TicketRepository:ITicketRepository
    {
        private readonly QueueContext queueContext;
        public TicketRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public async Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            queueContext.TicketEntity.Add(ticketEntity);
            await queueContext.SaveChangesAsync(cancellationToken);
            return ticketEntity.Id;
        }
        public async Task<bool> UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return true;
        }
        public async Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)//true грузим обработанные талоны false необработанные талоны
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return new();
        }

        public async Task <bool> SaveLastTicketNumberAsync(int number, string prefix, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return true;
        }

        public async Task<int> GetLastTicketNumberAsync(string prefix, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Thread.Sleep(100); });//просто заглушка чтоб студия не ругалась на async
            return 0;
        }

        public async Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId)
        {

            var ticketOperations = from ticketOperation in queueContext.TicketOperationEntity
                                   join windowTarget in queueContext.WindowTargetEntity
                                   on ticketOperation.TargetId equals windowTarget.TargetId
                                   where windowTarget.WindowId == windowId
                                   select ticketOperation.FromEntityToModel();

            return await ticketOperations.ToListAsync();
        }

        public async Task<TicketOperation?> GetTicketOperationByTicketAsync(long ticketId)
        {
            var ticketOperationEntity = await queueContext.TicketOperationEntity.FirstOrDefaultAsync(to => to.TicketId == ticketId);
            return ticketOperationEntity.FromEntityToModel();
        }

        public TicketOperation? GetTicketOperationByTicket(long ticketId)
        {
            var ticketOperationEntity = queueContext.TicketOperationEntity.FirstOrDefault(to => to.TicketId == ticketId);
            return ticketOperationEntity.FromEntityToModel();
        }

        public async Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity)
        {
            queueContext.TicketOperationEntity.Add(ticketOperationEntity);
            await queueContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity)
        {
            var ticketOperationEntityInDb = await queueContext.TicketOperationEntity.FirstOrDefaultAsync(toe=>toe.TicketId==ticketOperationEntity.TicketId);
            if(ticketOperationEntityInDb != null)
            {
                ticketOperationEntityInDb = ticketOperationEntity;
                await queueContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<TicketAmount?> GetTicketAmountAsync(long targetId)
        {
            var target = await queueContext.TargetEntity.SingleOrDefaultAsync(t => t.Id == targetId);
            var ticketAmount = await queueContext
                        .TicketAmountEntity
                        .SingleOrDefaultAsync(ta => ta.Prefix == target!.Prefix);
            if(ticketAmount == null)
            {
                ticketAmount = await CreateTicketAmountRecordAsync(target!.Prefix);
            }
            return ticketAmount.FromEntityToModel();
        }

        private async Task<TicketAmountEntity> CreateTicketAmountRecordAsync(char prefix)
        {
            TicketAmountEntity ticketAmountEntity = new TicketAmountEntity()
            {
                Prefix = prefix,
                Number = 1
            };
            queueContext.TicketAmountEntity.Add(ticketAmountEntity);
            await queueContext.SaveChangesAsync();
            return ticketAmountEntity;
        }
    }
}
