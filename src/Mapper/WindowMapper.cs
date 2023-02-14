using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
namespace bbqueue.Mapper
{
    internal static class WindowMapper
    {
        public static Window? FromEntityToModel(this WindowEntity? windowEntity)
        {
            return windowEntity != null ? new Window
            {
                Id = windowEntity.Id,
                Number = windowEntity.Number,
                Description = windowEntity.Description,
                EmployeeId = windowEntity.EmployeeId ?? throw new NullReferenceException("The value of 'windowEntity.EmployeeId' should not be null"),
                Employee = windowEntity.Employee != null ? new Employee
                {
                    Id = windowEntity.Employee.Id,
                    ExternalSystemIdentity = windowEntity.Employee.ExternalSystemIdentity,
                    Name = windowEntity.Employee.Name,
                    Active = windowEntity.Employee.Active,
                    Role = windowEntity.Employee.Role
                } : null
            } : null;
        }

        public static WindowEntity? FromModelToEntity(this Window? window)
        {
            return window != null ? new WindowEntity
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
                    Active = window.Employee.Active,
                    Role = window.Employee.Role
                } : null
            } : null;
        }
    }
}
