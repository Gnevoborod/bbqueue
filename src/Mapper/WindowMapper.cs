using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
namespace bbqueue.Mapper
{
    internal static class WindowMapper
    {
        public static Window? FromEntityToModel(this WindowEntity? windowEntity)
        {
            if (windowEntity == null)
                return null;
            return new Window
            {
                Id = windowEntity.Id,
                Number = windowEntity.Number,
                Description = windowEntity.Description,
                EmployeeId = windowEntity.EmployeeId,
                Employee = windowEntity.Employee != null ? new Employee
                {
                    Id = windowEntity.Employee.Id,
                    ExternalSystemIdentity = windowEntity.Employee.ExternalSystemIdentity,
                    Name = windowEntity.Employee.Name,
                    Active = windowEntity.Employee.Active
                } : null
            };
        }

        public static WindowEntity? FromModelToEntity(this Window? window)
        {
            if (window == null)
                return null;
            return new WindowEntity
            {
                Id = window.Id,
                Number = window.Number,
                Description = window.Description,
                EmployeeId = window.EmployeeId,
                Employee = window.Employee != null ? new EmployeeEntity
                {
                    Id = window.Employee.Id,
                    ExternalSystemIdentity = window.Employee.ExternalSystemIdentity,
                    Name = window.Employee.Name,
                    Active = window.Employee.Active
                } : null
            };
        }
    }
}
