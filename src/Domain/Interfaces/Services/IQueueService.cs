using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IQueueService
    {
        public Task AddTicketToQueueAsync(Ticket ticket);
        public Task ReturnTicketToQueueAsync(Ticket ticket, CancellationToken cancellationToken);

        public Task RemoveTicketAsync(Ticket ticket, CancellationToken cancellationToken);

        public Task ClearQueueAsync(CancellationToken cancellationToken);

        public Task<Ticket> GetTicketNextTicketFromQueueAsync(long windowId, CancellationToken cancellationToken);

        public Task<Ticket> GetNextSpecificTicketFromQueueAsync(long ticketNumber, CancellationToken cancellationToken);

        public Task RestoreQueueAsync(CancellationToken cancellationToken);

    }
}
