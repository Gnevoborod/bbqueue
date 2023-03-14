using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken);//
    }
}