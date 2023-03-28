using bbqueue.Controllers.Dtos.Error;
using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Infrastructure.Extensions;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace bbqueue.Controllers
{
    [Route("api/queue")]
    [Produces("application/json")]
    [ApiController]
    public class QueueController : Controller
    {
        private readonly IQueueService queueService;
        private readonly ITicketService ticketService;
        public QueueController(IQueueService queueService, ITicketService ticketService)
        {
            this.queueService = queueService;
            this.ticketService = ticketService;
        }


        /// <summary>
        /// Вызов следующего талона к определённому окну
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(TicketDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("nextCustomer")]
        public async Task<IActionResult> GetNextTicket()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            long employeeId = HttpContext.User.GetUserId();
            var ticket = await queueService.GetTicketNextTicketFromQueueAsync(employeeId, cancellationToken);
            if(ticket !=null)
            {
                await ticketService.TakeTicketToWork(ticket, employeeId, cancellationToken);
            }
            return Ok(ticket?.FromModelToDto());
        }
    }
}
