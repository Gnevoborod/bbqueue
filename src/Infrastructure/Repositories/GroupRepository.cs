using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using bbqueue.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using bbqueue.Controllers.Dtos.Group;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class GroupRepository : IGroupRepository
    {
        IServiceProvider serviceProvider;
        public GroupRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<List<Group>>? GetGroupsAsync(CancellationToken cancellationToken)
        {
            var queueContext=serviceProvider.GetService<QueueContext>();
            return await queueContext?.GroupEntity?
                    .OrderByDescending(g => g.GroupLinkId)
                    .Select(g=> g.FromEntityToModel()!)
                    .ToListAsync()!;
            
        }
    }
}
