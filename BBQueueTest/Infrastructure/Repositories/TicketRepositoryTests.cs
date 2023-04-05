using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using bbqueue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NLog.Targets;
using Shouldly;
using AutoFixture;

namespace BBQueueTest.Infrastructure.Repositories
{
    public class TicketRepositoryTests : IDisposable
    {
        private ITicketRepository ticketRepository;
        private QueueContext queueContext;

        public TicketRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<QueueContext>()
        .UseInMemoryDatabase(databaseName: "bbqueue_test_repository")
        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;
            queueContext = new QueueContext(options);
            queueContext.Database.EnsureDeleted();
            queueContext.Database.EnsureCreated();
            ticketRepository = new TicketRepository(queueContext, null);
            PrepareDatabase();
        }

        //ниже пробую AutoFixture
        [Theory]
        [InlineData(TicketState.InProcess)]
        public async void ChangeTicketStateTest(TicketState ticketState)
        {
            Fixture fixture = new Fixture();
            TicketEntity ticket = fixture.Create<TicketEntity>();
            ticket.Id = 1;
            ticket.TargetId = 2;
            queueContext.TicketEntity.Add(ticket);
            await queueContext.SaveChangesAsync();

            ticket.State = ticketState;
            await queueContext.SaveChangesAsync();

            await ticketRepository.UpdateTicketInDbAsync(ticket.FromEntityToModel(), CancellationToken.None);
            
            var newTicket = queueContext.TicketEntity.Where(te=>te.Id == 1).SingleOrDefault();
            newTicket.State.ShouldBe(ticketState);

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
            await queueContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            queueContext.Dispose();
        }
    }
}
