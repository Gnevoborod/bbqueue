using Xunit;
using NSubstitute;
using bbqueue.Domain.Interfaces.Services;
using Shouldly;
using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Infrastructure.Services;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BBQueueTest
{
    public class TicketServiceTests : IDisposable
    {
        private ITicketService ticketService;
        private ITicketRepository ticketRepository;

        private QueueContext queueContext;

        public TicketServiceTests()
        {
            var options = new DbContextOptionsBuilder<QueueContext>()
                .UseInMemoryDatabase(databaseName: "bbqueue_test")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            queueContext = new QueueContext(options);
            queueContext.Database.EnsureDeleted();
            queueContext.Database.EnsureCreated();

            PrepareDatabase();

            ticketRepository = new TicketRepository(queueContext, null);
            ticketService = new TicketService(ticketRepository, null);
        }
        [Theory]
        [InlineData(1)]
        public async void CreateTicketPublicNumberTest(long targetId)
        {
            var ticket = await ticketService.CreateTicketAsync(targetId, CancellationToken.None);
            ticket.Number.ShouldBe(1);
            ticket.PublicNumber.ShouldBe("Ш1");
            ticket.State.ShouldBe(bbqueue.Domain.Models.TicketState.Created);
            ticket.TargetId.ShouldBe(targetId);
        }


        public void Dispose()
        {
            queueContext.Dispose();
        }


        private void PrepareDatabase()
        {
            TargetEntity targetEntity = new()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Prefix = 'Ш'
            };
            queueContext.TargetEntity.Add(targetEntity);
            queueContext.SaveChanges();
        }
    }
}
