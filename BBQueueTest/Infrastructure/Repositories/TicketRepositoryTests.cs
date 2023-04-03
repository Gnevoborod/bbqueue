using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shouldly;

namespace BBQueueTest.Infrastructure.Repositories
{
    public class TicketRepositoryTests : IDisposable
    {
        private ITicketRepository ticketRepository;
        private QueueContext queueContext;

        private Ticket? preparedTicket;
        public TicketRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<QueueContext>()
        .UseInMemoryDatabase(databaseName: "bbqueue_test")
        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;
            queueContext = new QueueContext(options);
            queueContext.Database.EnsureDeleted();
            queueContext.Database.EnsureCreated();
            ticketRepository = new TicketRepository(queueContext, null);
            PrepareDatabase();
        }

        [Theory]
        [InlineData(TicketState.InProcess)]
        public async void ChangeTicketStateTest(TicketState ticketState)
        {
            preparedTicket.State = ticketState;
            await ticketRepository.UpdateTicketInDbAsync(preparedTicket, CancellationToken.None);
            var ticket = await ticketRepository.GetTicketByIdAsync(preparedTicket.Id, CancellationToken.None);

            ticket.State.ShouldBe(ticketState);

        }

        private async void PrepareDatabase()
        {
            long targetId = 2;
            TargetEntity targetEntity = new()
            {
                Id = targetId,
                Name = "Test_2",
                Description = "Test_2",
                Prefix = 'Г'
            };
            queueContext.TargetEntity.Add(targetEntity);
            queueContext.SaveChanges();

            preparedTicket = await ticketRepository.CreateTicketAsync(targetId, CancellationToken.None);
        }


        public void Dispose()
        {
            queueContext.Dispose();
        }
    }
}
