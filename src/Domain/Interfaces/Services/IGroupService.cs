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
    }
}