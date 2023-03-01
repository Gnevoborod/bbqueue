using Microsoft.AspNetCore.Mvc;
using bbqueue.Controllers.Dtos.Employee;
using bbqueue.Domain.Interfaces.Services;

namespace bbqueue.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        IServiceProvider serviceProvider;

        public EmployeeController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /* Авторизация - тонкий момент. По идее вначале должен идти запрос во внешнюю систему, где лежат учётные данные пользователей.
         * В той системе происходить авторизация и аутентификация, после чего в нашу систему будет прилетать просто идентификатор пользователя
         * во внешней системе, и на основании этого идентификатора будет выпускаться jwt. Но нам бы ещё сюда прикрутить подтверждение валидности ключа и перепыпуск ключа
         */
        [HttpGet("jwt")]
        public async Task<IActionResult> GetJWT([FromQuery] string employeeExternalId, CancellationToken cancellationToken)
        {
            return Ok(new JwtDto
                {
                Token = await serviceProvider
                        .GetService<IEmployeeService>()?
                        .GetJwtAsync(employeeExternalId,cancellationToken)!
                });
        }
    }
}
