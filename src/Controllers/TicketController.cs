using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAuth = bbqueue.Domain.Interfaces.Services.IAuthorizationService;

namespace bbqueue.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IAuth authorizationService;

        public TicketController(ITicketService ticketService, IAuth authorizationService)
        {
            this.ticketService = ticketService;
            this.authorizationService = authorizationService;
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> GetTicketAsync([FromQuery]long TargetCode)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var ticket = await ticketService.CreateTicketAsync(TargetCode, cancellationToken);
            return Ok(ticket.FromModelToDto());
        }

        [Authorize]
        [HttpPost("redirect")]
        public async Task<IActionResult> RedirectTicketToAnotherWindowAsync([FromBody] TicketRedirectDto ticketRedirectDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await ticketService.ChangeTicketTarget(ticketRedirectDto.TicketId, ticketRedirectDto.TargetId, authorizationService.GetUserId(HttpContext),  cancellationToken);
            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetTicketListAsync()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var result = await ticketService.LoadTicketsAsync(false, cancellationToken);
            return Ok(result);
        }

    }
}
