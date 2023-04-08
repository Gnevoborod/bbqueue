using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Поставляет информацию о разделах и подразделах
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken);//

        /// <summary>
        /// Создаёт новый раздел или подраздел
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public Task<long> AddGroupAsync(Group group);
    }
}