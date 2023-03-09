using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/window")]
    [ApiController]
    public sealed class WindowController : ControllerBase
    {
        private readonly IWindowService windowService;

        public WindowController(IWindowService windowService)
        {
            this.windowService = windowService;
        }

        [Authorize]
        [HttpPost("work_state")]
        public async Task<IActionResult> ChangeWindowWorkStateAsync([FromBody] ChangeWindowWorkStateDto dto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var window = dto.FromChangeStateDtoToModel();
            await windowService.ChangeWindowWorkStateAsync(window, cancellationToken);
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
