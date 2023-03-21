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
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class QueueController : Controller
    {
        private readonly IQueueService queueService;
        private readonly ITicketService ticketService;
        private readonly ILogger<QueueController> logger;
        public QueueController(IQueueService queueService, ITicketService ticketService, ILogger<QueueController> logger)
        {
            this.queueService = queueService;
            this.ticketService = ticketService;
            this.logger = logger;
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
            logger.LogInformation($"Инициирован процесс вызова следующего посетителя с талоном. Инициатор employeeId = {employeeId}");
               var ticket = await queueService.GetTicketNextTicketFromQueueAsync(employeeId, cancellationToken);
               if(ticket !=null)
               {
                   await ticketService.TakeTicketToWork(ticket, employeeId, cancellationToken);
               }
            logger.LogInformation($"Процесс вызова следующего посетителя с талоном для employeeId = {employeeId} завершён");
            return Ok(ticket?.FromModelToDto());
        }
    }
}
