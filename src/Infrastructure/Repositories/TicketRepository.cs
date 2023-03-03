using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class TicketRepository:ITicketRepository
    {
        IServiceProvider serviceProvider;
        public TicketRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken)
        {
            var context = serviceProvider.GetService<QueueContext>();
            context?.TicketEntity!.Add(ticketEntity!);
            await context?.SaveChangesAsync()!;
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
            var context = serviceProvider.GetService<QueueContext>();

            var ticketOperations = from ticketOperation in context?.TicketOperationEntity
                                   join windowTarget in context?.WindowTargetEntity!
                                   on ticketOperation.TargetId equals windowTarget.TargetId
                                   where windowTarget.WindowId == windowId
                                   select ticketOperation.FromEntityToModel()!;

            return await ticketOperations.ToListAsync();
        }

        public async Task<TicketOperation?> GetTicketOperationByTicketAsync(long ticketId)
        {
            var context = serviceProvider.GetService<QueueContext>();
            var ticketOperationEntity = await context?.TicketOperationEntity!.FirstOrDefaultAsync(to => to.TicketId == ticketId)!;
            return ticketOperationEntity.FromEntityToModel();
        }

        public TicketOperation? GetTicketOperationByTicket(long ticketId)
        {
            var context = serviceProvider.GetService<QueueContext>();
            var ticketOperationEntity = context?.TicketOperationEntity!.FirstOrDefault(to => to.TicketId == ticketId)!;
            return ticketOperationEntity.FromEntityToModel();
        }

        public async Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity)
        {
            var context = serviceProvider.GetService<QueueContext>();
            context?.TicketOperationEntity!.Add(ticketOperationEntity);
            await context?.SaveChangesAsync()!;
        }
        public async Task<bool> UpdateTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity)
        {
            var context = serviceProvider.GetService<QueueContext>();
            var ticketOperationEntityInDb = await context?.TicketOperationEntity!.FirstOrDefaultAsync(toe=>toe.TicketId==ticketOperationEntity.TicketId)!;
            if(ticketOperationEntityInDb != null)
            {
                ticketOperationEntityInDb = ticketOperationEntity;
                await context?.SaveChangesAsync()!;
                return true;
            }
            return false;
        }

        public async Task<TicketAmount> GetTicketAmountAsync(long targetId)
        {
            var context = serviceProvider.GetService<QueueContext>();
            var target = await context?.TargetEntity?.SingleOrDefaultAsync(t => t.Id == targetId)!;
            var ticketAmount = await context?
                        .TicketAmountEntity?
                        .SingleOrDefaultAsync(ta => ta.Prefix == target!.Prefix)!;
            if(ticketAmount == null)
            {
                ticketAmount = await CreateTicketAmountRecordAsync(target!.Prefix);
            }
            return ticketAmount.FromEntityToModel()!;
        }

        private async Task<TicketAmountEntity> CreateTicketAmountRecordAsync(char prefix)
        {
            var context = serviceProvider.GetService<QueueContext>();
            TicketAmountEntity ticketAmountEntity = new TicketAmountEntity()
            {
                Prefix = prefix,
                Number = 1
            };
            context?.TicketAmountEntity?.Add(ticketAmountEntity);
            await context?.SaveChangesAsync()!;
            return ticketAmountEntity;
        }
    }
}
