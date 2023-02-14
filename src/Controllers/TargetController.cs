using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        [HttpGet]
        [Route("targets")]
        public IActionResult GetTargets() {
            return Ok("Скоро тут будут цели");//Заглушка, тут надо вызывать сервис, поставляющий данные по таргетам
        }

        [HttpGet]
        [Route("sections")]
        public IActionResult GetHierarchy()
        {
            return Ok("Скоро тут будет структура с иерархией");//И ещё одна заглушка
        }
    }
}
