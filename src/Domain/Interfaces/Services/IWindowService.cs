using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IWindowService
    {
        /// <summary>
        /// Меняет состояние окна (Открыто, Перерыв, Закрыто)
        /// </summary>
        /// <param name="window"></param>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChangeWindowWorkStateAsync(Window window, long employeeId, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет список окон
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);

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