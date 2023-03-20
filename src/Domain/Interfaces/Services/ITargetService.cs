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
    }
}