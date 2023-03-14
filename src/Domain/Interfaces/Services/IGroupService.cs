using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IGroupService
    {
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken);
    }
}