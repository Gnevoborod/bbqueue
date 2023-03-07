using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IQueueService
    {
        public Task ReturnTicketToQueue(Ticket ticket, CancellationToken cancellationToken);

        public Task RemoveTicket(Ticket ticket, CancellationToken cancellationToken);

        public Task ClearQueue(CancellationToken cancellationToken);

        public Task<Ticket?> GetTicketNextTicketFromQueue(long windowId, CancellationToken cancellationToken);

        public Task<Ticket> GetNextSpecificTicketFromQueue(long ticketNumber, CancellationToken cancellationToken);

        public Task RestoreQueue(CancellationToken cancellationToken);
    }
}
