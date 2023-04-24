using EmployeeService.Controllers.Dtos.Authorization;
using EmployeeService.Controllers.Dtos.Error;
using EmployeeService.Domain.Interfaces.Services;
using EmployeeService.Infrastructure.Exceptions;
using EmployeeService.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [ApiController]
    [Produces("application/json")]
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
        /// <summary>
        /// Поставляет JWT токен для конкретного пользователя
        /// </summary>
        /// <param name="employeeExternalId">Код пользователя во внешней системе (основной системе в которой осуществляется авторизация)</param>
        /// <returns></returns>
        [HttpGet("jwt")]
        [ProducesResponseType(typeof(JwtDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
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
