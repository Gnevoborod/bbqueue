using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
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
            await Task.Run(() => { Thread.Sleep(100);});//просто заглушка чтоб студия не ругалась на async
            throw new NotImplementedException();
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
