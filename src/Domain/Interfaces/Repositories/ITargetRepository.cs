using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface ITargetRepository
    {
        /// <summary>
        /// Поставляет информацию о целях
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет новую цель в БД
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Task<long> AddTargetAsync(Target target);
    }
}