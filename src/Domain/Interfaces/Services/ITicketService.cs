using bbqueue.Database.Entities;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface ITicketService
    {
        /// <summary>
        /// Создаёт талон вместе со всеми связями
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket> CreateTicketAsync(long targetId, CancellationToken cancellationToken);

        /// <summary>
        /// Менят цель у талона
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="targetId"></param>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChangeTicketTarget(long ticketId, long targetId, long employeeId, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет список талонов
        /// </summary>
        /// <param name="loadOnlyProcessedTickets"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Ticket>> LoadTicketsAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);

        /// <summary>
        /// Переводит талон в состояние "В работе"
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="windowId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task TakeTicketToWork(Ticket ticket, long windowId, CancellationToken cancellationToken);

        /// <summary>
        /// Переводит талон в состояние "Закрыт"
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task CloseTicket(long ticketId, long userId, CancellationToken cancellationToken);

    }
}
