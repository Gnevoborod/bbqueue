using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
namespace bbqueue.Mapper
{
    internal static class TicketOperationMapper
    {
        public static TicketOperation? FromEntityToModel(this TicketOperationEntity? ticketOperationEntity)
        {
            return ticketOperationEntity != null ? new TicketOperation
            {
                Id = ticketOperationEntity.Id,
                TicketId = ticketOperationEntity.TicketId,
                Ticket = new Ticket
                {
                    Id = ticketOperationEntity.Ticket.Id,
                    Number = ticketOperationEntity.Ticket.Number,
                    State = ticketOperationEntity.Ticket.State,
                    Created = ticketOperationEntity.Ticket.Created,
                    Closed = ticketOperationEntity.Ticket.Closed
                },
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
            } : null;
        }

        public static TicketOperationEntity? FromModelToEntity(this TicketOperation? ticketOperation)
        {
            return ticketOperation != null ? new TicketOperationEntity
            {
                Id = ticketOperation.Id,
                TicketId = ticketOperation.TicketId,
                Ticket = new TicketEntity
                {
                    Id = ticketOperation.Ticket.Id,
                    Number = ticketOperation.Ticket.Number,
                    State = ticketOperation.Ticket.State,
                    Created = ticketOperation.Ticket.Created,
                    Closed = ticketOperation.Ticket.Closed
                },
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
                    } : null
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
            } : null;
        }
    }
}
