using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IWindowRepository
    {
        /// <summary>
        /// Меняет состояние окна
        /// </summary>
        /// <param name="window"></param>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangeWindowWorkStateAsync(Window window,  long employeeId, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет список окон
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет номер окна в привязке к сотруднику
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Window> GetWindowByEmployeeId(long employeeId, CancellationToken cancellationToken);
    }
}