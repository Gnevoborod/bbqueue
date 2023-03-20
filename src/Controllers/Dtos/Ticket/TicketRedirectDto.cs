using bbqueue.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace bbqueue.Controllers.Dtos.Ticket
{
    public sealed class TicketRedirectDto
    {
        [Range(1, long.MaxValue)]
        public long TargetId { get; set; } //идентификатор цели
        [Range(1, long.MaxValue)]
        public long TicketId { get; set; } //идентификатор талона
    }
}
