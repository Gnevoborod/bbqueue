using Infrastructure.AMQP.Messages;
using MassTransit;

namespace bbqueue.Infrastructure.AMQP.Consumers
{
    public class JwtKeyConsumer: IConsumer<JwtMessage>
    {
        private readonly ILogger<JwtKeyConsumer> logger;
        public JwtKeyConsumer(ILogger<JwtKeyConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<JwtMessage> consumeContext)
        {
            logger.LogInformation("Считан следующий ключ из очереди {0}", consumeContext.Message.Key);
            AuthOptions.RenewSymmetricSecurityKey(consumeContext.Message.Key);
            await Task.CompletedTask;
        }
    }
}
