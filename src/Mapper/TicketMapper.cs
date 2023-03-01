using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Mapper
{
    internal static class TicketMapper
    {
        public static Ticket? FromEntityToModel(this TicketEntity? ticketEntity)
        {
            return ticketEntity != null ? new Ticket
            {
                Id = ticketEntity.Id,
                Number = ticketEntity.Number,
                PublicNumber = ticketEntity.PublicNumber,
                State = ticketEntity.State,
                Created = ticketEntity.Created,
                Closed = ticketEntity.Closed
            } : null;
        }

        public static TicketEntity? FromModelToEntity(this Ticket? ticket)
        {
            return ticket != null ? new TicketEntity
            {
                Id = ticket.Id,
                Number = ticket.Number,
                PublicNumber = ticket.PublicNumber,
                State = ticket.State,
                Created = ticket.Created,
                Closed = ticket.Closed
            } : null;
        }
    }
}
