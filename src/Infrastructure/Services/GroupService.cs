using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class GroupService : IGroupService
    {
        private readonly IServiceProvider serviceProvider;
        public GroupService(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<List<Group>>? GetGroupsAsync(CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<IGroupRepository>()?.GetGroupsAsync(cancellationToken)!;
        }
    }
}
