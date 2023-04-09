using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IGroupService
    {
        /// <summary>
        /// Поставляет список разделов и подразделов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Создаёт новый раздел или подраздел
        /// </summary>
        /// <param name="group"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<long> AddGroupAsync(Group group, CancellationToken cancellationToken);
    }
}