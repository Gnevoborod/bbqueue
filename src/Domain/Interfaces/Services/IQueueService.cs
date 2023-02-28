﻿using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface IQueueService
    {
        public Task<bool> AddTicket(Ticket ticket, CancellationToken cancellationToken);
        public Task<bool> ReturnTicketToQueue(Ticket ticket, CancellationToken cancellationToken);

        public Task<bool> RemoveTicket(Ticket ticket, CancellationToken cancellationToken);

        public Task ClearQueue(CancellationToken cancellationToken);

        public Task<Ticket> GetTicketNextTicketFromQueue(long windowId, CancellationToken cancellationToken);

        public Task<Ticket> GetNextSpecificTicketFromQueue(long ticketNumber, CancellationToken cancellationToken);

        public Task RestoreQueue(CancellationToken cancellationToken);
    }
}
