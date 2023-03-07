using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> GetTicketAsync([FromQuery]long TargetCode)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var ticket = await ticketService.CreateTicketAsync(TargetCode, cancellationToken)!;
            return Ok(ticket.FromModelToDto());
        }

        [HttpPost("redirect")]
        public async Task<IActionResult> RedirectTicketToAnotherWindowAsync([FromBody] TicketRedirectDto ticketRedirectDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await ticketService.ChangeTicketTarget(ticketRedirectDto.TicketNumber, ticketRedirectDto.TargetCode, cancellationToken)!;
            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetTicketListAsync([FromQuery] long EmployeeCode)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var result = await ticketService.LoadTicketsAsync(false, cancellationToken)!;
            return Ok(result);
        }

    }
}
