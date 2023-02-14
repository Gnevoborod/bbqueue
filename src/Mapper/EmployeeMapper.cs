using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Mapper
{
    internal static class EmployeeMapper
    {
        public static Employee? FromEntityToModel(this EmployeeEntity? employeeEntity)
        {
            return employeeEntity != null ? new Employee
            {
                Id = employeeEntity.Id,
                ExternalSystemIdentity = employeeEntity.ExternalSystemIdentity,
                Name = employeeEntity.Name,
                Active = employeeEntity.Active,
                Role = employeeEntity.Role
            } : null;
        }

        public static EmployeeEntity? FromModelToEntity(this Employee? employee)
        {
            return employee != null ? new EmployeeEntity
            {
                Id = employee.Id,
                ExternalSystemIdentity = employee.ExternalSystemIdentity,
                Name = employee.Name,
                Active = employee.Active,
                Role = employee.Role
            } : null;
        }
    }
}
