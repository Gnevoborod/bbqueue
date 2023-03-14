﻿using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Mapper
{
    internal static class TicketMapper
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
                Created = ticket.Created
            };
        }
    }
}
