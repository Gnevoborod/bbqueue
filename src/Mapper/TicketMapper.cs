using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using System.Net.Sockets;

namespace bbqueue.Mapper
{
    public static class TicketMapper
    {
        public static Ticket FromEntityToModel(this TicketEntity ticketEntity)
        {
            if (ticketEntity == null)
                return default!;
            return new Ticket
            {
                Id = ticketEntity.Id,
                Number = ticketEntity.Number,
                PublicNumber = ticketEntity.PublicNumber,
                State = ticketEntity.State,
                TargetId = ticketEntity.TargetId,
                Target = ticketEntity.Target == null? default! : new Target
                {
                    Id = ticketEntity.Target.Id,
                    Name = ticketEntity.Target.Name,
                    Description = ticketEntity.Target.Description,
                    Prefix = ticketEntity.Target.Prefix,
                    GroupLinkId = ticketEntity.Target.GroupLinkId,
                    GroupLink = ticketEntity.Target.GroupLink != null ? new Group
                    {
                        Id = ticketEntity.Target.GroupLink.Id,
                        Name = ticketEntity.Target.GroupLink.Name,
                        Description = ticketEntity.Target.GroupLink.Description,
                        GroupLinkId = ticketEntity.Target.GroupLink.GroupLinkId,
                        GroupLink = null /* Stop recursive mapping */
                    } : null
                },
                Created = ticketEntity.Created,
                Closed = ticketEntity.Closed
            };
        }

        public static TicketEntity FromModelToEntity(this Ticket ticket)
        {
            if (ticket == null)
                return default!;
            return new TicketEntity
            {
                Id = ticket.Id,
                Number = ticket.Number,
                PublicNumber = ticket.PublicNumber,
                State = ticket.State,
                TargetId = ticket.TargetId,
                Target = ticket.Target ==null? default! : new TargetEntity
                {
                    Id = ticket.Target.Id,
                    Name = ticket.Target.Name,
                    Description = ticket.Target.Description,
                    Prefix = ticket.Target.Prefix,
                    GroupLinkId = ticket.Target.GroupLinkId,
                    GroupLink = ticket.Target.GroupLink != null ? new GroupEntity
                    {
                        Id = ticket.Target.GroupLink.Id,
                        Name = ticket.Target.GroupLink.Name,
                        Description = ticket.Target.GroupLink.Description,
                        GroupLinkId = ticket.Target.GroupLink.GroupLinkId,
                        GroupLink = null /* Stop recursive mapping */
                    } : null
                },
                Created = ticket.Created,
                Closed = ticket.Closed
            };
        }

        public static TicketDto FromModelToDto(this Ticket ticket)
        {
            if (ticket == null)
                return default!;
            return new TicketDto
            {
                Id = ticket.Id,
                Number = ticket.Number,
                PublicNumber = ticket.PublicNumber,
                TargetId = ticket.TargetId,
                Created = ticket.Created
            };
        }
    }
}
