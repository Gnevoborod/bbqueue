using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IAuth = bbqueue.Domain.Interfaces.Services.IAuthorizationService;
namespace bbqueue.Controllers
{
    [Route("api/window")]
    [ApiController]
    public sealed class WindowController : ControllerBase
    {
        private readonly IWindowService windowService;
        private readonly IAuth authrizationService;

        public WindowController(IWindowService windowService, IAuth authorizationService)
        {
            this.windowService = windowService;
            this.authrizationService = authorizationService;
        }

        [Authorize]
        [HttpPost("work_state")]
        public async Task<IActionResult> ChangeWindowWorkStateAsync([FromBody] ChangeWindowWorkStateDto dto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var window = dto.FromChangeStateDtoToModel();
            var employeeId = authrizationService.GetUserId(HttpContext);
            await windowService.ChangeWindowWorkStateAsync(window, employeeId,  cancellationToken);
            return Ok();
        }


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
