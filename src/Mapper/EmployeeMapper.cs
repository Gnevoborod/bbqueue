using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Mapper
{
    internal static class EmployeeMapper
    {
        public static Employee? FromEntityToModel(this EmployeeEntity? employeeEntity)
        {
            if (employeeEntity == null)
                return null;
            return new Employee
            {
                Id = employeeEntity.Id,
                ExternalSystemIdentity = employeeEntity.ExternalSystemIdentity,
                Name = employeeEntity.Name,
                Active = employeeEntity.Active
            };
        }

        public static EmployeeEntity? FromModelToEntity(this Employee? employee)
        {
            if (employee == null)
                return null;
            return new EmployeeEntity
            {
                Id = employee.Id,
                ExternalSystemIdentity = employee.ExternalSystemIdentity,
                Name = employee.Name,
                Active = employee.Active
            };
        }
    }
}
