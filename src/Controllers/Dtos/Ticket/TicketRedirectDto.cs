using bbqueue.Infrastructure.Validators;
namespace bbqueue.Controllers.Dtos.Ticket
{
    public sealed class TicketRedirectDto
    {
        [MinValue(1)]
        public long TargetCode { get; set; } //идентификатор цели
        [MinValue(1)]
        public long TicketNumber { get; set; } //идентификатор талона
    }
}
