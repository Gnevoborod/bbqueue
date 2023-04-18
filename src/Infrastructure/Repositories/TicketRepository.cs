using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class TicketRepository : ITicketRepository
    {
        private readonly QueueContext queueContext;
        private readonly ILogger<TicketRepository> logger;
        public TicketRepository(QueueContext queueContext, ILogger<TicketRepository> logger)
        {
            this.queueContext = queueContext;
            this.logger = logger;
        }
        private async Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, char prefix, CancellationToken cancellationToken)
        {
            queueContext.TicketEntity.Add(ticketEntity);
            await SaveLastTicketNumberAsync(ticketEntity.Number, prefix, cancellationToken);
            await queueContext.SaveChangesAsync(cancellationToken);
            return ticketEntity.Id;
        }

        public async Task<Ticket?> GetTicketByIdAsync(long ticketId, CancellationToken cancellationToken)
        {
            var ticket = await queueContext.TicketEntity.SingleOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
            return ticket == null ? default! : ticket.FromEntityToModel();
        }

        public async Task UpdateTicketInDbAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            var ticketEntity = await queueContext.TicketEntity.FirstOrDefaultAsync(te => te.Id == ticket.Id, cancellationToken);
            if (ticketEntity == null)
            {
                throw new ApiException(ExceptionEvents.TicketNotFound);
            }
            ticketEntity.State = ticket.State;
            ticketEntity.Closed = ticket.Closed;
            ticketEntity.TargetId = ticket.TargetId;
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken)//true грузим обработанные талоны false необработанные талоны
        {
            return queueContext.TicketEntity
                .Where(te=> loadOnlyProcessedTickets? te.State==TicketState.Closed:te.State!=TicketState.Closed)
                .Select(te => te.FromEntityToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task SaveLastTicketNumberAsync(int number, char prefix, CancellationToken cancellationToken)
        {
            var ticketAmount = await queueContext.TicketAmountEntity.SingleOrDefaultAsync(tae => tae.Prefix == prefix && tae.Number == number - 1, cancellationToken);
            if (ticketAmount == null)
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
            return queueContext.TicketOperationEntity.Where(to => to.TicketId == ticketId).Select(to => to.FromEntityToModel()).ToListAsync(cancellationToken);
        }

        public Task SaveTicketOperationToDbAsync(TicketOperation ticketOperation, CancellationToken cancellationToken)
        {
            queueContext.TicketOperationEntity.Add(ticketOperation.FromModelToEntity());
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
            if (ticketAmount == null)
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
            var window = await WindowRelatedToEmployeeasync(employeeId, cancellationToken);
            if (window == null)
            {
                throw new ApiException(ExceptionEvents.TicketUnableToTakeToWork);
            }
            var query = await (from te in queueContext.TicketEntity
                               join
                               wte in queueContext.WindowTargetEntity
                               on te.TargetId equals wte.TargetId
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
            {
                throw new ApiException(ExceptionEvents.TicketUnableToTakeToWork);
            }
            var ticket = await queueContext.TicketEntity.SingleOrDefaultAsync(te => te.Id == ticketId, cancellationToken);
            return ticket == null ? default! : ticket.FromEntityToModel();
        }


        private Task<WindowEntity?> WindowRelatedToEmployeeasync(long employeeId, CancellationToken cancellationToken)
        {
            return (from win in queueContext.WindowEntity
                    where win.EmployeeId == employeeId
                    select win).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteAllTicketsFromDBAsync(CancellationToken cancellationToken)
        {
            int MAX_TICKETS_TO_DELETE = 1000;
            try
            { 
                logger.BeginScope("Запуск автоочистки данных по талонам");
                queueContext.RemoveRange(queueContext.TicketAmountEntity);//тут всегда конечное количество записей - 30, так что можем не беспокоиться о производительности
                logger.LogInformation("Очистка данных о последних номерах талонов");
                List<TicketEntity> ticket = default!;
                while((ticket = queueContext.TicketEntity.Where(te => te.Created > DateTime.UtcNow.AddMonths(1)).Take(MAX_TICKETS_TO_DELETE).ToList()).Count>0)
                {
                    using var transaction = queueContext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    var ticketIds = ticket.Select(t=>t.Id).ToList();
                    queueContext.RemoveRange(queueContext.TicketOperationEntity.Where(to => ticketIds.Contains(to.TicketId)).Take(MAX_TICKETS_TO_DELETE));
                    logger.LogInformation("Очистка лога операций с талонами");
                    queueContext.RemoveRange(ticket);
                    logger.LogInformation("Удаление талонов");
                    await queueContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                logger.LogInformation("Очистка успешно завершена");
            }
            catch
            {
                throw new ApiException(ExceptionEvents.TicketCleaningFailed);
            }
        }

        public async Task AddTicketOperation(TicketOperation ticketOperation, Ticket ticket, CancellationToken cancellationToken)
        {
            using var transaction = queueContext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            try
            {
                await SaveTicketOperationToDbAsync(ticketOperation, cancellationToken);
                await UpdateTicketInDbAsync(ticket, cancellationToken);
                await transaction.CommitAsync();
            }
            catch
            {
                throw new ApiException(ExceptionEvents.TicketTakeToWorkTransactionFailed);
            }
        }

        public async Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken)
        {

            var ticketAmount = await GetTicketAmountAsync(targetId, cancellationToken);
            if (ticketAmount == null)
            {
                throw new ApiException(ExceptionEvents.TargetPrefixUndefined);
            }
            TicketEntity ticket = new()
            {
                State = TicketState.Created,
                Created = DateTime.UtcNow
            };
            char prefix = ticketAmount.Prefix;
            int nextNumber = ++ticketAmount.Number;
            ticket.Number = nextNumber;
            ticket.PublicNumber = prefix + nextNumber.ToString();
            ticket.TargetId = targetId;


            using var transaction = queueContext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            try
            {
                ticket.Id = await SaveTicketToDbAsync(ticket, prefix, cancellationToken);

                await SaveTicketOperationToDbAsync(new TicketOperation()
                {
                    TicketId = ticket.Id,
                    TargetId = targetId,
                    State = TicketState.Created,
                    Processed = DateTime.UtcNow

                }, cancellationToken);

                await transaction.CommitAsync();
                return ticket.FromEntityToModel();
            }
            catch
            {
                throw new ApiException(ExceptionEvents.TicketUnableToCreate);
            }
        }
    }
}
