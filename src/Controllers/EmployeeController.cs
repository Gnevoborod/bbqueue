using Microsoft.AspNetCore.Mvc;
using bbqueue.Controllers.Dtos.Employee;
using bbqueue.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using bbqueue.Mapper;
using bbqueue.Domain.Models;
using bbqueue.Controllers.Dtos.Error;
using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Infrastructure.Extensions;

using IAuth = bbqueue.Domain.Interfaces.Services.IAuthorizationService;
using bbqueue.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace bbqueue.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/employee")]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }

        /// <summary>
        /// Поставляет информацию о сотруднике
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(TicketDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployeeInfo(int employeeId)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var employee = await employeeService.GetEmployeeInfoAsync(employeeId, cancellationToken);
            if (employee == null)
            {
                logger.LogError(ExceptionEvents.EmployeeNotFound, ExceptionEvents.EmployeeNotFound.Name + $" employeeId = {employeeId}");
                throw new ApiException(ExceptionEvents.EmployeeNotFound);
            }
            return Ok(employee.FromModelToDto());
        }

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        /// <param name="employeeRegistryDto">Структура содержащая информацию о пользователе</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("employee_add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRegistryDto employeeRegistryDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            logger.LogInformation($"Инициировано создание записи о сотруднике {employeeRegistryDto.Name}, с внешним идентификатором {employeeRegistryDto.ExternalSystemId}");
            await employeeService.AddEmployeeAsync(employeeRegistryDto.FromDtoToModel(), cancellationToken);
            logger.LogInformation($"Создание записи о сотруднике {employeeRegistryDto.Name} завершено");
            return Ok();
        }

        /// <summary>
        /// Устанавливает сотруднику роль
        /// </summary>
        /// <param name="employeeSetRoleDto">Структура содержащая идентификатор сотрудника и название роли</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [HttpPost("role_set")]
        public async Task<IActionResult> SetRole([FromBody] EmployeeSetRoleDto employeeSetRoleDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            logger.LogInformation($"Инициирована установка роли для сотрудника c employeeId = {employeeSetRoleDto.EmployeeId}, роль = {employeeSetRoleDto.Role}");
            var role = EmployeeMapper.EmployeeRoleFromDtoToValue(employeeSetRoleDto.Role);
            if (role == null)
            {
                logger.LogError(ExceptionEvents.WrongRoleInRequest, ExceptionEvents.WrongRoleInRequest.Name + $". Role is = {employeeSetRoleDto.Role}");
                throw new ApiException(ExceptionEvents.WrongRoleInRequest);
            }
            await employeeService.SetRoleToEmployeeAsync(employeeSetRoleDto.EmployeeId, (EmployeeRole)role, cancellationToken);
            logger.LogInformation($"Установка роли для сотрудника c employeeId = {employeeSetRoleDto.EmployeeId} завершена");
            return Ok();
        }

        /// <summary>
        /// Поставляет список ролей в строковом представлении
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        [ProducesResponseType(typeof(EmployeeRoleInfoListDto), 200)]
        public IActionResult GetRoles()
        {
            return Ok(new EmployeeRoleInfoListDto()
            {
                EmployeeRoles = Enum.GetNames(typeof(EmployeeRole)).ToList()
            });
        }

        /// <summary>
        /// Прикрепляет сотрудника к окну
        /// </summary>
        /// <param name="employeeToWindowDto">Структура содержит идентификатор окна для привязки сотрудника (а id сотрудника берём из JWT)</param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [HttpPost("employee_to_window")]
        public async Task<IActionResult> AddEmployeeToWindow([FromBody] EmployeeToWindowDto employeeToWindowDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var userId = HttpContext.User.GetUserId();
            logger.LogInformation($"Инициирована привязка сотрудника с employeeId = {userId} к окну {employeeToWindowDto.WindowId}");
            await employeeService.AddEmployeeToWindowAsync(userId, employeeToWindowDto.WindowId, cancellationToken);
            logger.LogInformation($"Привязка сотрудника с employeeId = {userId} к окну {employeeToWindowDto.WindowId} завершена");
            return Ok();
        }

    }
}
