using bbqueue.Controllers.Dtos.Window;
using bbqueue.Domain.Models;
using System.Threading;

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

        /// <summary>
        /// Добавление нового окна
        /// </summary>
        /// <param name="window"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<long> AddNewWindowAsync(Window window, CancellationToken cancellationToken);

        /// <summary>
        /// Назначает цель к окну
        /// </summary>
        /// <param name="WindowId"></param>
        /// <param name="TargetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddTargetToWindowAsync(long WindowId, long TargetId, CancellationToken cancellationToken);
    }
}