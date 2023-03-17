using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using bbqueue.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using bbqueue.Controllers.Dtos.Group;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class GroupRepository : IGroupRepository
    {
        private readonly QueueContext queueContext;
        public GroupRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            return queueContext.GroupEntity
                    .OrderByDescending(g => g.GroupLinkId)
                    .Select(g=> g.FromEntityToModel())
                    .ToListAsync(cancellationToken);
            
        }
    }
}
