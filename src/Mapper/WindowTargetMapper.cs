using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
namespace bbqueue.Mapper
{
    internal static class WindowTargetMapper
    {
        public static WindowTarget FromEntityToModel(this WindowTargetEntity windowTargetEntity)
        {
            if (windowTargetEntity == null)
                return default!;
            return new WindowTarget
            {
                Id = windowTargetEntity.Id,
                WindowId = windowTargetEntity.WindowId,
                Window = new Window
                {
                    Id = windowTargetEntity.Window.Id,
                    Number = windowTargetEntity.Window.Number,
                    Description = windowTargetEntity.Window.Description,
                    EmployeeId = windowTargetEntity.Window.EmployeeId,
                    Employee = windowTargetEntity.Window.Employee != null ? new Employee
                    {
                        Id = windowTargetEntity.Window.Employee.Id,
                        ExternalSystemIdentity = windowTargetEntity.Window.Employee.ExternalSystemIdentity,
                        Name = windowTargetEntity.Window.Employee.Name,
                        Active = windowTargetEntity.Window.Employee.Active,
                        Role = windowTargetEntity.Window.Employee.Role
                    } : null,
                    WindowWorkState = windowTargetEntity.Window.WindowWorkState
                },
                TargetId = windowTargetEntity.TargetId,
                Target = new Target
                {
                    Id = windowTargetEntity.Target.Id,
                    Name = windowTargetEntity.Target.Name,
                    Description = windowTargetEntity.Target.Description,
                    Prefix = windowTargetEntity.Target.Prefix,
                    GroupLinkId = windowTargetEntity.Target.GroupLinkId,
                    GroupLink = windowTargetEntity.Target.GroupLink != null ? new Group
                    {
                        Id = windowTargetEntity.Target.GroupLink.Id,
                        Name = windowTargetEntity.Target.GroupLink.Name,
                        Description = windowTargetEntity.Target.GroupLink.Description,
                        GroupLinkId = windowTargetEntity.Target.GroupLink.GroupLinkId,
                        GroupLink =null /* Stop recursive mapping */
                    } : null
                }
            };
        }

        public static WindowTargetEntity FromModelToEntity(this WindowTarget windowTarget)
        {
            if (windowTarget == null)
                return default!;
            return new WindowTargetEntity
            {
                Id = windowTarget.Id,
                WindowId = windowTarget.WindowId,
                Window = new WindowEntity
                {
                    Id = windowTarget.Window.Id,
                    Number = windowTarget.Window.Number,
                    Description = windowTarget.Window.Description,
                    EmployeeId = windowTarget.Window.EmployeeId,
                    Employee = windowTarget.Window.Employee != null ? new EmployeeEntity
                    {
                        Id = windowTarget.Window.Employee.Id,
                        ExternalSystemIdentity = windowTarget.Window.Employee.ExternalSystemIdentity,
                        Name = windowTarget.Window.Employee.Name,
                        Active = windowTarget.Window.Employee.Active,
                        Role = windowTarget.Window.Employee.Role
                    } : null,
                    WindowWorkState = windowTarget.Window.WindowWorkState
                },
                TargetId = windowTarget.TargetId,
                Target = new TargetEntity
                {
                    Id = windowTarget.Target.Id,
                    Name = windowTarget.Target.Name,
                    Description = windowTarget.Target.Description,
                    Prefix = windowTarget.Target.Prefix,
                    GroupLinkId = windowTarget.Target.GroupLinkId,
                    GroupLink = windowTarget.Target.GroupLink != null ? new GroupEntity
                    {
                        Id = windowTarget.Target.GroupLink.Id,
                        Name = windowTarget.Target.GroupLink.Name,
                        Description = windowTarget.Target.GroupLink.Description,
                        GroupLinkId = windowTarget.Target.GroupLink.GroupLinkId,
                        GroupLink = null /* Stop recursive mapping */
                    } : null
                }
            };
        }
    }
}
