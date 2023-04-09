using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using bbqueue.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using bbqueue.Controllers.Dtos.Group;
using bbqueue.Infrastructure.Exceptions;
using System.Threading;

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

        public async Task<long> AddGroupAsync(Group group, CancellationToken cancellationToken)
        {
            GroupEntity? groupInDb = default!;
            if (group.GroupLinkId != null)
            {
                groupInDb = await queueContext.GroupEntity.FirstOrDefaultAsync(ge => ge.Id == group.GroupLinkId, cancellationToken);
                if (groupInDb == null)
                {
                    throw new ApiException(ExceptionEvents.GroupParentIdNotExists);
                }
            }

            groupInDb = await queueContext.GroupEntity.FirstOrDefaultAsync(ge=>ge.Name == group.Name, cancellationToken);
            if(groupInDb!=null)
            {
                throw new ApiException(ExceptionEvents.GroupNameExists);
            }


            var newGroup = group.FromModelToEntity();
            queueContext.GroupEntity.Add(newGroup);
            await queueContext.SaveChangesAsync(cancellationToken);
            return newGroup.Id;

        }
    }
}
