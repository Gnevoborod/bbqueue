using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Infrastructure.Repositories;
using NCrontab;

namespace bbqueue.Infrastructure.Jobs
{
    public class TicketsCleanHostedService:BackgroundService
    {
        private readonly ILogger<TicketsCleanHostedService> logger;
        //private readonly ITicketRepository ticketRepository;
        private readonly IServiceProvider serviceProvider;
        public TicketsCleanHostedService(ILogger<TicketsCleanHostedService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            //this.ticketRepository = ticketRepository;
            this.serviceProvider = serviceProvider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            while(!cancellationToken.IsCancellationRequested)
            {
                logger.BeginScope("Очередной старт работы крона");
                var cron = CrontabSchedule.Parse("0 07 * * 1");
                var nextOccurrence = cron.GetNextOccurrence(DateTime.Now);
                var delay = nextOccurrence - DateTime.Now;
                await Task.Delay(delay, cancellationToken);

                //Без этого костыля будем получать ошибку
                using var scope = serviceProvider.CreateScope();
                var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
                try
                {
                    await ticketRepository.DeleteAllTicketsFromDBAsync(cancellationToken);
                    logger.LogInformation("Успешное завершение выполнения задачи");
                }
                catch (Exception ex)
                {
                    logger.LogError(ExceptionEvents.HostedServiceErrorWhileExecuted, ex.Message + " " + ex.StackTrace);
                }
            }
        }


    
    }
}
