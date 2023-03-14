using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [Route("api/queue")]
    [ApiController]
    public class QueueController : Controller
    {
        

        public QueueController()
        {
        }
        [HttpPost("nextCustomer")]
        public async Task<IActionResult> GetNextTicket()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await Task.Run(() => { Thread.Sleep(1000); });
            return Ok();
        }
    }
}
