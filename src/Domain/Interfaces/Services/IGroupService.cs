using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface IGroupService
    {
        Task<List<Group>>? GetGroupsAsync(CancellationToken cancellationToken);
    }
}