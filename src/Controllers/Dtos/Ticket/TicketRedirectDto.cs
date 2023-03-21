using bbqueue.Infrastructure.Validators;
namespace bbqueue.Controllers.Dtos.Ticket
{
    public sealed class TicketRedirectDto
    {
        [MinValue(1)]
        public long TargetId { get; set; } //идентификатор цели
        [MinValue(1)]
        public long TicketId { get; set; } //идентификатор талона
    }
}
