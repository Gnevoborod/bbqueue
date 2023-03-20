using bbqueue.Domain.Models;
using bbqueue.Database.Entities;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        /// <summary>
        /// Сохраняет талон в БД
        /// </summary>
        /// <param name="ticketEntity"></param>
        /// <param name="prefix"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<long> SaveTicketToDbAsync(TicketEntity ticketEntity, char prefix, CancellationToken cancellationToken);
        /// <summary>
        /// Обновляет талон в БД
        /// </summary>
        /// <param name="ticketEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UpdateTicketInDbAsync(TicketEntity ticketEntity, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет список талонов из БД
        /// </summary>
        /// <param name="loadOnlyProcessedTickets">Если true то поставляет обработанные талоны, если false необработанные талоны</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Ticket>> LoadTicketsFromDbAsync(bool loadOnlyProcessedTickets, CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет номер последнего выданного талона в разрезе префиксов (чтобы в будущем корректно формировать новый номер талона при выдаче)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="prefix"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SaveLastTicketNumberAsync(int number, char prefix, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет список операций для конкретного окна
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<TicketOperation>> GetTicketOperationByWindowPlusTargetAsync(long windowId, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет операцию для конкретного талона
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<TicketOperation>> GetTicketOperationByTicket(long ticketId, CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет операцию по талону
        /// </summary>
        /// <param name="ticketOperationEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SaveTicketOperationToDbAsync(TicketOperationEntity ticketOperationEntity, CancellationToken cancellationToken);
        /// <summary>
        /// Возвращает количество выданных талонов (для формирования нового номера талона)
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TicketAmount> GetTicketAmountAsync(long targetId, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет следующий талон для сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket?> GetNextTicketAsync(long employeeId, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет следующий конкретный талон для сотрудника
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket?> GetNextSpecificTicketAsync(long ticketId, long employeeId, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет все талоны (и все связанные данные по талонам)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteAllTicketsFromDBAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет талон по идентификатору (без привязки к сотруднику)
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Ticket?> GetTicketByIdAsync(long ticketId, CancellationToken cancellationToken);
    }
}
