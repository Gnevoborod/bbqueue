using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using bbqueue.Controllers.Dtos.Window;
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
                EmployeeId = windowEntity.EmployeeId,
                Employee = windowEntity.Employee != null ? new Employee
                {
                    Id = windowEntity.Employee.Id,
                    ExternalSystemIdentity = windowEntity.Employee.ExternalSystemIdentity,
                    Name = windowEntity.Employee.Name,
                    Active = windowEntity.Employee.Active,
                    Role = windowEntity.Employee.Role
                } : null,
                WindowWorkState = windowEntity.WindowWorkState
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
                } : null,
                WindowWorkState = window.WindowWorkState
            } : null;
        }

        public static WindowDto? FromModelToDto(this Window? window)
        {
            return window != null ? new WindowDto
            {
                Id = window.Id,
                Number = window.Number,
                Description = window.Description
            } : null;
        }

        public static Window? FromChangeStateDtoToModel(this ChangeWindowWorkStateDto? dto)
        {
            return dto != null ? new Window
            {
                Number = dto.Number,
                WindowWorkState = dto.WindowWorkState
            } : null;
        }
    }
}
