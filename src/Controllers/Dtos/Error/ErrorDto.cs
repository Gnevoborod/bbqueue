namespace bbqueue.Controllers.Dtos.Error
{
    public class ErrorDto
    {
        public string Code { get; private set; } = default!;
        public string Message { get; private set; } = default!;
        public string TraceId { get; private set; } = default!;
    }
}
