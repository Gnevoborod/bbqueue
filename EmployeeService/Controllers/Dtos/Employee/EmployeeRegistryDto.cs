using EmployeeService.Domain.Models;
using EmployeeService.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Controllers.Dtos.Employee
{
    public class EmployeeRegistryDto
    {
        [Required, MaxLength(16)]
        public string ExternalSystemId { get; set; } = default!;
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        public bool Active { get; set; }
        [EnumValueScope(typeof(EmployeeRole))]

        public string Role { get; set; } = default!;
    }
}
