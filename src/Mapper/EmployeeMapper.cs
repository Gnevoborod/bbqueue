using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bbqueue.Mapper
{
    internal static class EmployeeMapper
    {
        public static Employee? FromEntityToModel(this DbContext dbContext, EmployeeEntity employeeEntity)
        {
            if (employeeEntity == null)
                return null;
            return FromEntityToModelMapper(employeeEntity);
        }

        public static List<Employee>? FromEntitiesToModels(this DbContext dbContext, List<EmployeeEntity> employeeEntities)
        {
            if (employeeEntities == null)
                return null;
            List<Employee> employees = new List<Employee>(employeeEntities.Count);
            foreach(var entity in employeeEntities)
            {
                employees.Add(FromEntityToModelMapper(entity));
            }
            return employees;
        }

        public static Employee? FromEntityToModel(this EmployeeEntity employeeEntity)
        {
            if (employeeEntity == null)
                return null;
            return FromEntityToModelMapper(employeeEntity);
        }

        private static Employee FromEntityToModelMapper(EmployeeEntity employeeEntity)
        {
            return new Employee
            {
                Id = employeeEntity.Id,
                Name = employeeEntity.Name,
                ExternalSystemIdentity = employeeEntity.ExternalSystemIdentity,
                Active = employeeEntity.Active
            };
        }
    }
}
