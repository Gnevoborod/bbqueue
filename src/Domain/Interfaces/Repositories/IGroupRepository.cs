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
    }
}