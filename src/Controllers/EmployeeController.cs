using Microsoft.AspNetCore.Mvc;
using bbqueue.Controllers.Dtos.Employee;
using bbqueue.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using bbqueue.Mapper;
using bbqueue.Domain.Models;
using Microsoft.OpenApi.Extensions;
using System.Security.Principal;
using System.Security.Claims;

namespace bbqueue.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [Authorize]
        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployeeInfo(int employeeId)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var employee = await employeeService.GetEmployeeInfoAsync(employeeId, cancellationToken);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee.FromModelToDto());
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("employee_add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRegistryDto employeeRegistryDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            await employeeService.AddEmployeeAsync(employeeRegistryDto.FromDtoToModel(), cancellationToken);
            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("role_set")]
        public async Task<IActionResult> SetRole([FromBody] EmployeeSetRoleDto employeeSetRoleDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var role = EmployeeMapper.EmployeeRoleFromDtoToValue(employeeSetRoleDto.Role);
            if (role == null)
                throw new Exception("Некорректно указана роль");
            await employeeService.SetRoleToEmployeeAsync(employeeSetRoleDto.EmployeeId, (EmployeeRole)role, cancellationToken);
            return Ok();
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            return Ok(new EmployeeRoleInfoListDto()
            {
                EmployeeRoles = Enum.GetNames(typeof(EmployeeRole)).ToList()
            });
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("employee_to_window")]
        public async Task<IActionResult> AddEmployeeToWindow([FromBody] EmployeeToWindowDto employeeToWindowDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;

            await employeeService.AddEmployeeToWindowAsync(employeeToWindowDto.EmployeeId, employeeToWindowDto.WindowId, cancellationToken);
            return Ok();
        }

    }
}
