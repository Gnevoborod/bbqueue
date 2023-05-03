namespace Infrastructure.AMQP.Messages
{
    //такой неймспейс поскольку в противном случае в exchange у сообщений будет отличаться тип (именно неймспейсом) и в результате будет создаваться _skipped очередь
    public class JwtMessage
    {
        public string Key { get; set; } = default!;
    }
}
