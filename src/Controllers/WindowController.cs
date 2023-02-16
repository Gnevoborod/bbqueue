using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/window")]
    [ApiController]
    public sealed class WindowController : ControllerBase
    {
        [HttpPost]
        [Route("work_state")]
        public IActionResult ChangeWindowWorkState([FromBody] ChangeWindowWorkStateDto dto)
        {
            var window = dto.FromChangeStateDtoToModel();
            if(window != null) { 
            var result=new WindowService().ChangeWindowWorkState(window);
            if (result) 
                return Ok();
            }
            return BadRequest();//тут по-хорошему надо что-то внятное возвращать
        }

        
        [HttpGet]
        [Route("windows")]
        public IActionResult GetWindows()
        {
            var windows=new WindowService().GetWindows();
            WindowListDto windowListDto = new WindowListDto(windows.Count());
            foreach(var window in windows)
            {
                windowListDto?.Windows?.Add(window.FromModelToDto()!);
            }
            return Ok(windowListDto);
        }
    }
}
