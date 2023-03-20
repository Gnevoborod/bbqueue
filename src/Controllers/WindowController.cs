using bbqueue.Controllers.Dtos.Error;
using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Infrastructure.Extensions;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAuth = bbqueue.Domain.Interfaces.Services.IAuthorizationService;
namespace bbqueue.Controllers
{
    [Route("api/window")]
    [Produces("application/json")]
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public sealed class WindowController : ControllerBase
    {
        private readonly IWindowService windowService;
        private readonly IAuth authrizationService;
        public WindowController(IWindowService windowService, IAuth authorizationService)
        {
            this.windowService = windowService;
            this.authrizationService = authorizationService;
        }

        /// <summary>
        /// Меняет состояние окна (Открыто, Перерыв, Закрыто)
        /// </summary>
        /// <param name="dto">На вход принимает номер окна и состояние (строковое значение)</param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [HttpPost("work_state")]
        public async Task<IActionResult> ChangeWindowWorkStateAsync([FromBody] ChangeWindowWorkStateDto dto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var window = dto.FromChangeStateDtoToModel();
            var employeeId = HttpContext.User.GetUserId();
            await windowService.ChangeWindowWorkStateAsync(window, employeeId,  cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Возвращает список окон
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(WindowListDto), 200)]
        [HttpGet("windows")]
        public async Task<IActionResult> GetWindowsAsync()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var windows = await windowService.GetWindowsAsync(cancellationToken);
            WindowListDto windowListDto = new()
            {
                Windows = windows.Select(w => w.FromModelToDto()).ToList()
            };
            return Ok(windowListDto);
        }
    }
}
