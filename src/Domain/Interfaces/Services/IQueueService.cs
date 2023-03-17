using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IQueueService
    {
        public Task ClearQueueAsync(CancellationToken cancellationToken);

        public Task<Ticket?> GetTicketNextTicketFromQueueAsync(long employeeId, CancellationToken cancellationToken);

        public Task<Ticket?> GetNextSpecificTicketFromQueueAsync(long ticketNumber, long employeeId, CancellationToken cancellationToken);

    }
}
