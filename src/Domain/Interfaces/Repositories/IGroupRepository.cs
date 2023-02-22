using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    internal interface IGroupRepository
    {
        Task<List<Group>>? GetGroupsAsync(CancellationToken cancellationToken);
    }
}