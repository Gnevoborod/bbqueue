using EmployeeService.Controllers.Dtos.Employee;
using EmployeeService.Database.Entities;
using EmployeeService.Domain.Models;

namespace EmployeeService.Mapper
{
    internal static class EmployeeMapper
    {
        public static Employee FromEntityToModel(this EmployeeEntity employeeEntity)
        {
            if (employeeEntity == null)
                return default!;
            return new Employee
            {
                Id = employeeEntity.Id,
                ExternalSystemIdentity = employeeEntity.ExternalSystemIdentity,
                Name = employeeEntity.Name,
                Active = employeeEntity.Active,
                Role = employeeEntity.Role
            };
        }

        public static EmployeeEntity FromModelToEntity(this Employee employee)
        {
            if (employee == null)
                return default!;
            return new EmployeeEntity
            {
                Id = employee.Id,
                ExternalSystemIdentity = employee.ExternalSystemIdentity,
                Name = employee.Name,
                Active = employee.Active,
                Role = employee.Role
            };
        }

        public static Employee FromDtoToModel(this EmployeeRegistryDto employeeRegistryDto)
        {
            var role = EmployeeRoleFromDtoToValue(employeeRegistryDto.Role);
            return new Employee
            {
                ExternalSystemIdentity = employeeRegistryDto.ExternalSystemId,
                Name = employeeRegistryDto.Name,
                Active = employeeRegistryDto.Active,
                Role = role == null? default! : (EmployeeRole)role
            };
        }


        public static EmployeeRole? EmployeeRoleFromDtoToValue(string dtoRole)
        {
            var enumItems = Enum.GetNames(typeof(EmployeeRole));
            var enumItemsIndexes = Enum.GetValues(typeof(EmployeeRole));
            EmployeeRole? role = default;
            for (int i = 0; i < enumItems.Length; i++)
            {
                if (enumItems[i].ToLower() == dtoRole.ToLower())
                    role = (EmployeeRole?)enumItemsIndexes.GetValue(i);
            }
            return role;
        }

        public static EmployeeDto FromModelToDto(this Employee employee)
        {
            if (employee == null)
                return default!;
            return new EmployeeDto
            {
                Id = employee.Id,
                ExternalSystemIdentity = employee.ExternalSystemIdentity,
                Name = employee.Name,
                Active = employee.Active,
                Role = employee.Role
            };
        }
    }
}
