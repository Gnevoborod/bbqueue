using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/window")]
    [ApiController]
    public sealed class WindowController : ControllerBase
    {
        IServiceProvider serviceProvider;

        public WindowController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Route("work_state")]
        public async Task<IActionResult> ChangeWindowWorkStateAsync([FromBody] ChangeWindowWorkStateDto dto, CancellationToken cancellationToken)
        {
            var window = dto.FromChangeStateDtoToModel();
            if(window != null) { 
            var result= await serviceProvider.GetService<IWindowService>()?.ChangeWindowWorkStateAsync(window, cancellationToken)!;
            if (result) 
                return Ok();
            }
            return BadRequest();//тут по-хорошему надо что-то внятное возвращать
        }


        [HttpGet]
        [Route("windows")]
        public async Task<IActionResult> GetWindowsAsync(CancellationToken cancellationToken)
        {
            var windows = await serviceProvider.GetService<IWindowService>()?.GetWindowsAsync(cancellationToken)!;
            cancellationToken.ThrowIfCancellationRequested();
            WindowListDto windowListDto = new()
            {
                Windows = windows?.Select(w => w.FromModelToDto()!).ToList()
            };
            return Ok(windowListDto);
        }
    }
}
