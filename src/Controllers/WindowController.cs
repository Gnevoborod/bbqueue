using bbqueue.Controllers.Dtos.Error;
using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure.Extensions;
using bbqueue.Infrastructure.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/window")]
    [Produces("application/json")]
    [ApiController]
    public sealed class WindowController : ControllerBase
    {
        private readonly IWindowService windowService;
        public WindowController(IWindowService windowService)
        {
            this.windowService = windowService;
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
            var window = dto.FromChangeStateDtoToModel();
            await windowService.ChangeWindowWorkStateAsync(window, employeeId, cancellationToken);
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

        /// <summary>
        /// Создаёт новое окно
        /// </summary>
        /// <param name="windowCreateDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(WindowCreatedIdDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("add_window")]
        public async Task<IActionResult> CreateNewWindowAsync(WindowCreateDto windowCreateDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var windowId = await windowService.AddNewWindowAsync(windowCreateDto.FromDtoToModel(), cancellationToken);
            return Ok(new WindowCreatedIdDto
            {
                WindowId = windowId
            });
        }

        /// <summary>
        /// Прикрепляет цель к окну
        /// </summary>
        /// <param name="windowTargetCreateDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("add_target_window")]
        public async Task <IActionResult> AddTargetToWindowAsync(WindowTargetCreateDto windowTargetCreateDto)
        {
            CancellationToken cancellationToken= HttpContext.RequestAborted;
            await windowService.AddTargetToWindowAsync(windowTargetCreateDto.WindowId, windowTargetCreateDto.TargetId, cancellationToken);
            return Ok();
        }


        /// <summary>
        /// Прикрепляет сотрудника к окну
        /// </summary>
        /// <param name="employeeToWindowDto">Структура содержит идентификатор окна для привязки сотрудника (а id сотрудника берём из JWT)</param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [HttpPost("employee_to_window")]
        public async Task<IActionResult> AddEmployeeToWindow([FromBody] EmployeeToWindowDto employeeToWindowDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var userId = HttpContext.User.GetUserId();
            await windowService.AddEmployeeToWindowAsync(userId, employeeToWindowDto.WindowId, cancellationToken);
            return Ok();
        }
    }
}
