using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IQueueService
    {
        /// <summary>
        /// Очищает очередь
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ClearQueueAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет следующий талон из очереди (в разрезе сотрудника)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket?> GetTicketNextTicketFromQueueAsync(long employeeId, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет следующий конкретный талон для сотрудника
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket?> GetNextSpecificTicketFromQueueAsync(long ticketNumber, long employeeId, CancellationToken cancellationToken);

    }
}
