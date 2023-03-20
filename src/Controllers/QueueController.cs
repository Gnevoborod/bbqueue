using bbqueue.Controllers.Dtos.Error;
using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthService = bbqueue.Domain.Interfaces.Services.IAuthorizationService;
namespace bbqueue.Controllers
{
    [Route("api/queue")]
    [Produces("application/json")]
    [ApiController]
    public class QueueController : Controller
    {
        private readonly IQueueService queueService;
        private readonly ITicketService ticketService;
        private readonly IAuthService authorizationService;

        public QueueController(IQueueService queueService, ITicketService ticketService, IAuthService authorizationService)
        {
            this.queueService = queueService;
            this.ticketService = ticketService;
            this.authorizationService = authorizationService;
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
            long employeeId = authorizationService.GetUserId(HttpContext);
               var ticket = await queueService.GetTicketNextTicketFromQueueAsync(employeeId, cancellationToken);
               if(ticket !=null)
               {
                   await ticketService.TakeTicketToWork(ticket, employeeId, cancellationToken);
               }
            return Ok(ticket?.FromModelToDto());
        }
    }
}
