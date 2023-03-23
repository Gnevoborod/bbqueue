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
        private readonly ILogger<WindowController> logger;
        public WindowController(IWindowService windowService, ILogger<WindowController> logger)
        {
            this.windowService = windowService;
            this.logger = logger;
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
            var employeeId = HttpContext.User.GetUserId();
            logger.LogInformation($"Инициирована смена состояния окна {dto.Number} сотрудником с employeeId = {employeeId}");
            var window = dto.FromChangeStateDtoToModel();
            await windowService.ChangeWindowWorkStateAsync(window, employeeId, cancellationToken);
            logger.LogInformation($"Cмена состояния окна {dto.Number} завершена");
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
