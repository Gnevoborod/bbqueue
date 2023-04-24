using Microsoft.AspNetCore.Mvc;
using EmployeeService.Controllers.Dtos.Employee;
using EmployeeService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using EmployeeService.Mapper;
using EmployeeService.Domain.Models;
using EmployeeService.Controllers.Dtos.Error;
using EmployeeService.Infrastructure.Extensions;

using EmployeeService.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace EmployeeService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Поставляет информацию о сотруднике
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
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
                throw new ApiException(ExceptionEvents.EmployeeNotFound);
            }
            return Ok(employee.FromModelToDto());
        }

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        /// <param name="employeeRegistryDto">Структура содержащая информацию о пользователе</param>
        /// <returns></returns>
        //[Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 401)]
        [HttpPost("employee_add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRegistryDto employeeRegistryDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await employeeService.AddEmployeeAsync(employeeRegistryDto.FromDtoToModel(), cancellationToken);
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
            var role = EmployeeMapper.EmployeeRoleFromDtoToValue(employeeSetRoleDto.Role);
            if (role == null)
            {
                throw new ApiException(ExceptionEvents.WrongRoleInRequest);
            }
            await employeeService.SetRoleToEmployeeAsync(employeeSetRoleDto.EmployeeId, (EmployeeRole)role, cancellationToken);
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


    }
}
