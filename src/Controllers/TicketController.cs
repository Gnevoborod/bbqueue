﻿using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Controllers.Dtos.Error;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAuth = bbqueue.Domain.Interfaces.Services.IAuthorizationService;

namespace bbqueue.Controllers
{
    [Route("api/ticket")]
    [Produces("application/json")]
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

        /// <summary>
        /// Выдаёт новый талон
        /// </summary>
        /// <param name="TargetId">Код цели</param>
        /// <returns></returns>
        [HttpGet("ticket")]
        [ProducesResponseType(typeof(TicketDto),200)]
        [ProducesResponseType(typeof(ErrorDto),400)]
        public async Task<IActionResult> GetTicketAsync([FromQuery]long TargetId)
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
        [ProducesResponseType(typeof(ErrorDto),401)]
        [HttpPost("redirect")]
        public async Task<IActionResult> RedirectTicketToAnotherWindowAsync([FromBody] TicketRedirectDto ticketRedirectDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await ticketService.ChangeTicketTarget(ticketRedirectDto.TicketId, ticketRedirectDto.TargetId, authorizationService.GetUserId(HttpContext),  cancellationToken);
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
