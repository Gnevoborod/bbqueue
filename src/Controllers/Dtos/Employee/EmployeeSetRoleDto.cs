using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Validators;

namespace bbqueue.Controllers.Dtos.Employee
{
    public class EmployeeSetRoleDto
    {
        public long EmployeeId { get; set; }

        [EnumValueScope(typeof(EmployeeRole))]
        public string Role { get; set; } = default!;
    }
}
