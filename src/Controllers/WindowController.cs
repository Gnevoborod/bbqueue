using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindowController : ControllerBase
    {
        [HttpPost]
        [Route("work_state")]
        public IActionResult ChangeWindowWorkState()
        {
            return Ok("Скоро тут будет реализация");//Заглушка
        }

        [HttpGet]
        [Route("windows")]
        public IActionResult GetWindows()
        {
            return Ok("Скоро тут будет реализация");//Заглушка
        }
    }
}
