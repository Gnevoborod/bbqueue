using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Mapper
{
    internal static class TicketMapper
    {
        public static Ticket? FromEntityToModel(this TicketEntity ticketEntity)
        {
            if (ticketEntity == null)
                return null;
            return new Ticket
            {
                Id=ticketEntity.Id,
                Number= ticketEntity.Number,
                Created = ticketEntity.Created,
                Closed= ticketEntity.Closed,
                State= ticketEntity.State
            };
        }

        public static TicketEntity? FromModelToEntity(this Ticket ticket)
        {
            if (ticket == null)
                return null;
            return new TicketEntity
            {
                Id = ticket.Id,
                Number = ticket.Number,
                Created = ticket.Created,
                Closed = ticket.Closed,
                State = ticket.State
            };
        }
    }
}
