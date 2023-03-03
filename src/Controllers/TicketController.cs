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

        IServiceProvider serviceProvider;

        public TicketController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> GetTicketAsync([FromQuery]long TargetCode, CancellationToken cancellationToken)
        {
            var ticket = await serviceProvider.GetService<ITicketService>()?.CreateTicketAsync(TargetCode, cancellationToken)!;
            return Ok(ticket.FromModelToDto());
        }

        [HttpPost("redirect")]
        public async Task<IActionResult> RedirectTicketToAnotherWindowAsync([FromBody] TicketRedirectDto ticketRedirectDto, CancellationToken cancellationToken)
        {
            bool result = await serviceProvider.GetService<ITicketService>()?.ChangeTicketTarget(ticketRedirectDto.TicketNumber, ticketRedirectDto.TargetCode, cancellationToken)!;
            if (result)
                return Ok();
            return BadRequest();//пока заглушка
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetTicketListAsync([FromQuery] long EmployeeCode, CancellationToken cancellationToken)
        {
            var result = await serviceProvider.GetService<ITicketService>()?.LoadTicketsAsync(false, cancellationToken)!;
            if(result.Count>0)
                return Ok(result);
            return BadRequest();
        }

    }
}
