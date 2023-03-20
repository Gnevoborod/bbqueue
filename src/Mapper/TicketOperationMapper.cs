using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using System.Net.Sockets;

namespace bbqueue.Mapper
{
    internal static class TicketOperationMapper
    {
        public static TicketOperation FromEntityToModel(this TicketOperationEntity ticketOperationEntity)
        {
            if (ticketOperationEntity == null)
                return default!;
            return new TicketOperation
            {
                Id = ticketOperationEntity.Id,
                TicketId = ticketOperationEntity.TicketId,
                Ticket = new Ticket
                {
                    Id = ticketOperationEntity.Ticket.Id,
                    Number = ticketOperationEntity.Ticket.Number,
                    PublicNumber = ticketOperationEntity.Ticket.PublicNumber,
                    State = ticketOperationEntity.Ticket.State,
                    TargetId = ticketOperationEntity.Ticket.TargetId,
                    Target = ticketOperationEntity.TargetEntity == null? default!: new Target
                    {
                        Id = ticketOperationEntity.Ticket.Target.Id,
                        Name = ticketOperationEntity.Ticket.Target.Name,
                        Description = ticketOperationEntity.Ticket.Target.Description,
                        Prefix = ticketOperationEntity.Ticket.Target.Prefix,
                        GroupLinkId = ticketOperationEntity.Ticket.Target.GroupLinkId,
                        GroupLink = ticketOperationEntity.Ticket.Target.GroupLink != null ? new Group
                        {
                            Id = ticketOperationEntity.Ticket.Target.GroupLink.Id,
                            Name = ticketOperationEntity.Ticket.Target.GroupLink.Name,
                            Description = ticketOperationEntity.Ticket.Target.GroupLink.Description,
                            GroupLinkId = ticketOperationEntity.Ticket.Target.GroupLink.GroupLinkId,
                            GroupLink = null /* Stop recursive mapping */
                        } : null
                    },
                    Created = ticketOperationEntity.Ticket.Created,
                    Closed = ticketOperationEntity.Ticket.Closed
                },
                TargetId = ticketOperationEntity.TargetId,
                WindowId = ticketOperationEntity.WindowId,
                Window = ticketOperationEntity.Window != null ? new Window
                {
                    Id = ticketOperationEntity.Window.Id,
                    Number = ticketOperationEntity.Window.Number,
                    Description = ticketOperationEntity.Window.Description,
                    EmployeeId = ticketOperationEntity.Window.EmployeeId,
                    Employee = ticketOperationEntity.Window.Employee != null ? new Employee
                    {
                        Id = ticketOperationEntity.Window.Employee.Id,
                        ExternalSystemIdentity = ticketOperationEntity.Window.Employee.ExternalSystemIdentity,
                        Name = ticketOperationEntity.Window.Employee.Name,
                        Active = ticketOperationEntity.Window.Employee.Active,
                        Role = ticketOperationEntity.Window.Employee.Role
                    } : null,
                    WindowWorkState = ticketOperationEntity.Window.WindowWorkState
                } : null,
                EmployeeId = ticketOperationEntity.EmployeeId,
                Employee = ticketOperationEntity.Employee != null ? new Employee
                {
                    Id = ticketOperationEntity.Employee.Id,
                    ExternalSystemIdentity = ticketOperationEntity.Employee.ExternalSystemIdentity,
                    Name = ticketOperationEntity.Employee.Name,
                    Active = ticketOperationEntity.Employee.Active,
                    Role = ticketOperationEntity.Employee.Role
                } : null,
                State = ticketOperationEntity.State,
                Processed = ticketOperationEntity.Processed
            };
        }

        public static TicketOperationEntity FromModelToEntity(this TicketOperation ticketOperation)
        {
            if (ticketOperation == null)
                return default!;
            return new TicketOperationEntity
            {
                Id = ticketOperation.Id,
                TicketId = ticketOperation.TicketId,
                Ticket = ticketOperation.Target == null? default! : new TicketEntity
                {
                    Id = ticketOperation.Ticket.Id,
                    Number = ticketOperation.Ticket.Number,
                    PublicNumber = ticketOperation.Ticket.PublicNumber,
                    State = ticketOperation.Ticket.State,
                    TargetId = ticketOperation.Ticket.TargetId,
                    Target = new TargetEntity
                    {
                        Id = ticketOperation.Ticket.Target.Id,
                        Name = ticketOperation.Ticket.Target.Name,
                        Description = ticketOperation.Ticket.Target.Description,
                        Prefix = ticketOperation.Ticket.Target.Prefix,
                        GroupLinkId = ticketOperation.Ticket.Target.GroupLinkId,
                        GroupLink = ticketOperation.Ticket.Target.GroupLink != null ? new GroupEntity
                        {
                            Id = ticketOperation.Ticket.Target.GroupLink.Id,
                            Name = ticketOperation.Ticket.Target.GroupLink.Name,
                            Description = ticketOperation.Ticket.Target.GroupLink.Description,
                            GroupLinkId = ticketOperation.Ticket.Target.GroupLink.GroupLinkId,
                            GroupLink = null /* Stop recursive mapping */
                        } : null
                    },
                    Created = ticketOperation.Ticket.Created,
                    Closed = ticketOperation.Ticket.Closed
                },
                TargetId = ticketOperation.TargetId,
                WindowId = ticketOperation.WindowId,
                Window = ticketOperation.Window != null ? new WindowEntity
                {
                    Id = ticketOperation.Window.Id,
                    Number = ticketOperation.Window.Number,
                    Description = ticketOperation.Window.Description,
                    EmployeeId = ticketOperation.Window.EmployeeId,
                    Employee = ticketOperation.Window.Employee != null ? new EmployeeEntity
                    {
                        Id = ticketOperation.Window.Employee.Id,
                        ExternalSystemIdentity = ticketOperation.Window.Employee.ExternalSystemIdentity,
                        Name = ticketOperation.Window.Employee.Name,
                        Active = ticketOperation.Window.Employee.Active,
                        Role = ticketOperation.Window.Employee.Role
                    } : null,
                    WindowWorkState = ticketOperation.Window.WindowWorkState
                } : null,
                EmployeeId = ticketOperation.EmployeeId,
                Employee = ticketOperation.Employee != null ? new EmployeeEntity
                {
                    Id = ticketOperation.Employee.Id,
                    ExternalSystemIdentity = ticketOperation.Employee.ExternalSystemIdentity,
                    Name = ticketOperation.Employee.Name,
                    Active = ticketOperation.Employee.Active,
                    Role = ticketOperation.Employee.Role
                } : null,
                State = ticketOperation.State,
                Processed = ticketOperation.Processed
            };
        }
    }
}
