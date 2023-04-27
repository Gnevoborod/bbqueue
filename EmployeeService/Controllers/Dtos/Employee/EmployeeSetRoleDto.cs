using EmployeeService.Domain.Models;
using EmployeeService.Infrastructure.Validators;

namespace EmployeeService.Controllers.Dtos.Employee
{
    public class EmployeeSetRoleDto
    {
        public long EmployeeId { get; set; }

        [EnumValueScope(typeof(EmployeeRole))]
        public string Role { get; set; } = default!;
    }
}
