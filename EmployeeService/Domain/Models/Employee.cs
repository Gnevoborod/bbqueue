using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domain.Models
{
    public enum EmployeeRole { Employee, Manager};
    public sealed class Employee
    {
        public long Id { get; set; }

        [MaxLength(16)]
        public string ExternalSystemIdentity { get; set; } = default!;

        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        public EmployeeRole Role { get; set; }
    }
}