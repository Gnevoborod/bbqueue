using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface ITargetService
    {
        /// <summary>
        /// Поставляет список разделов и подразделов вместе с целями
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<GroupHierarchyDto> GetHierarchyAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет список целей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет новую цель в БД
        /// </summary>
        /// <param name="target"></param>
        /// /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<long> AddTargetAsync(Target target, CancellationToken cancellationToken);
    }
}