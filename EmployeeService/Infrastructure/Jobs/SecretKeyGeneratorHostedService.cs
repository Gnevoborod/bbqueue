using Infrastructure.AMQP.Messages;
using EmployeeService.Infrastructure.Exceptions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using System.Threading;

namespace EmployeeService.Infrastructure.Jobs
{
    public class SecretKeyGeneratorHostedService : BackgroundService
    {
        private readonly ILogger<SecretKeyGeneratorHostedService> logger;
        private readonly IBus bus;
        private const string secretKeyBaseArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?()><'@#&";
        private const int secretKeyLength = 16;
        public SecretKeyGeneratorHostedService(ILogger<SecretKeyGeneratorHostedService> logger, IBus bus)
        {
            this.logger = logger;
            this.bus = bus;
            PushKeyEverywhere();//на старте приложения сразу отправляем ключ всем, так как следующее обновление ключа в 9 утра
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.BeginScope("Очередной старт работы крона");
                var cron = CrontabSchedule.Parse("0 9 * * *");//запускаем каждый день, так как номера талонов надо очищать ежедневно, а сами талоны удаляем по прошествии месяца после их создания
                var nextOccurrence = cron.GetNextOccurrence(DateTime.Now);
                var delay = nextOccurrence - DateTime.Now;
                await Task.Delay(delay, cancellationToken);
                try
                {
                    PushKeyEverywhere();
                }
                catch (Exception ex)
                {
                    logger.LogError(ExceptionEvents.SecretKeyGeneratorErrorWhileExecuted, ex.Message + " " + ex.StackTrace);
                }
            }
        }

        private string KeyGen()
        {
            Random rand = new Random();
            char[] result = new char[secretKeyLength];
            int next = 0;
            for (int i = 0; i < secretKeyLength; i++)
            {
                next = rand.Next(0, secretKeyBaseArray.Length);
                result[i] = secretKeyBaseArray[next];
            }
            return new string(result);
        }

        private void PushKeyEverywhere()
        {
            string newSecretKey = KeyGen();
            //а затем ставить в очередь
            AuthOptions.RenewSymmetricSecurityKey(newSecretKey);
         
            bus.Publish(new JwtMessage() { 
                Key = newSecretKey 
            });
            logger.LogInformation("Обновлённое значение секретного ключа отправлено в очередь: {0}", newSecretKey);
        }
    }
}
