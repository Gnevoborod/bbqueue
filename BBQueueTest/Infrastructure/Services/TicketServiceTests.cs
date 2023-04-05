using Xunit;
using NSubstitute;
using bbqueue.Domain.Interfaces.Services;
using Shouldly;
using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Infrastructure.Services;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using bbqueue.Infrastructure.Exceptions;
using NSubstitute.ReceivedExtensions;

namespace BBQueueTest
{
    public class TicketServiceTests : IDisposable
    {
        private ITicketService ticketService;
        private ITicketRepository ticketRepository;
        private IWindowRepository windowRepository;

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

            ticketRepository = Substitute.For<ITicketRepository>();
            windowRepository = Substitute.For<IWindowRepository>();
            ticketService = new TicketService(ticketRepository, windowRepository);
        }
        [Theory]
        [InlineData(5)]
        public async void ChangeTicketTarget_ThrownExceptionTest(long targetId)
        {
            //тест переписал, поскольку в создании талона, если мы мокаем талон, никакого смысла нет
            var exception = Should.Throw<ApiException>(async ()=>await ticketService.ChangeTicketTarget(-1, targetId, 0, CancellationToken.None));
            exception.HResult.ShouldBe(ExceptionEvents.TicketNotFound.Id);
        }


        [Theory]
        [InlineData(5)]
        public async void ChangeTicketTarget_SuccessTest(long targetId)
        {
            //тест переписал, поскольку в создании талона, если мы мокаем талон, никакого смысла нет

            ticketRepository.GetTicketByIdAsync(1, CancellationToken.None).ReturnsForAnyArgs(new Ticket()
            {
                Id = 1,
                TargetId = 1,
                Number = 1,
                PublicNumber = "Ж1",
                Created = DateTime.UtcNow,
                State = TicketState.Created
            });

            await ticketService.ChangeTicketTarget(-1, targetId, 0, CancellationToken.None);

            await ticketRepository.Received().GetTicketByIdAsync(-1, CancellationToken.None);
            await ticketRepository.Received().SaveTicketOperationToDbAsync(Arg.Any<TicketOperation>(), CancellationToken.None);
            await ticketRepository.Received().UpdateTicketInDbAsync(Arg.Any<Ticket>(), CancellationToken.None);
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
