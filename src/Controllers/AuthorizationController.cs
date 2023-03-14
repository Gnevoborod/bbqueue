using bbqueue.Controllers.Dtos.Employee;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace bbqueue.Controllers
{
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        /* Авторизация - тонкий момент. По идее вначале должен идти запрос во внешнюю систему, где лежат учётные данные пользователей.
         * В той системе происходить авторизация и аутентификация, после чего в нашу систему будет прилетать просто идентификатор пользователя
         * во внешней системе, и на основании этого идентификатора будет выпускаться jwt. Но нам бы ещё сюда прикрутить подтверждение валидности ключа и перепыпуск ключа
         */
        [HttpGet("jwt")]
        public async Task<IActionResult> GetJWT([FromQuery] string employeeExternalId)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            return Ok(new JwtDto
            {
                Token = await authorizationService
                        .GetJwtAsync(employeeExternalId, cancellationToken)
            });
        }
    }
}
