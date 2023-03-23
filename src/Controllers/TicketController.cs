using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Controllers.Dtos.Error;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using bbqueue.Infrastructure.Exceptions;
using bbqueue.Infrastructure.Extensions;

namespace bbqueue.Controllers
{
    [Route("api/ticket")]
    [Produces("application/json")]
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly ILogger<TicketController> logger;

        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            this.ticketService = ticketService;
            this.logger = logger;
        }

        /// <summary>
        /// Выдаёт новый талон
        /// </summary>
        /// <param name="TargetId">Код цели</param>
        /// <returns></returns>
        [HttpGet("ticket")]
        [ProducesResponseType(typeof(TicketDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetTicketAsync([FromQuery] long TargetId)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var ticket = await ticketService.CreateTicketAsync(TargetId, cancellationToken);
            return Ok(ticket.FromModelToDto());
        }


        /// <summary>
        /// Осуществляет перенаправление талона на другую цель
        /// </summary>
        /// <param name="ticketRedirectDto"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("redirect")]
        public async Task<IActionResult> RedirectTicketToAnotherWindowAsync([FromBody] TicketRedirectDto ticketRedirectDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            logger.LogInformation($"Redirecting ticket {ticketRedirectDto.TicketId} init.");
            await ticketService.ChangeTicketTarget(ticketRedirectDto.TicketId, ticketRedirectDto.TargetId, HttpContext.User.GetUserId(), cancellationToken);
            logger.LogInformation($"Redirecting ticket {ticketRedirectDto.TicketId} done.");
            return Ok();
        }


        /// <summary>
        /// Поставляет список необработанных талонов
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpGet("list")]
        public async Task<IActionResult> GetTicketListAsync()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var result = await ticketService.LoadTicketsAsync(false, cancellationToken);
            return Ok(result);
        }

    }
}
