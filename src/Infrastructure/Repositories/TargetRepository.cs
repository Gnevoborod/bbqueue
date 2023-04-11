using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class TargetRepository : ITargetRepository
    {
        private readonly QueueContext queueContext;
        public TargetRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken)
        {
            return queueContext
                .TargetEntity
                .OrderByDescending(g => g.GroupLinkId)
                .Select(t => t.FromEntityToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<long> AddTargetAsync(Target target, CancellationToken cancellationToken)
        {
            var targetInDb = await queueContext.TargetEntity.FirstOrDefaultAsync(te => te.Prefix == target.Prefix, cancellationToken);
            if(targetInDb != null)
            {
                throw new ApiException(ExceptionEvents.TargetPrefixExists);
            }
            targetInDb = await queueContext.TargetEntity.FirstOrDefaultAsync(te=>te.Name == target.Name, cancellationToken);
            if(targetInDb != null )
            {
                throw new ApiException(ExceptionEvents.TargetNameExists);
            }

            if (target.GroupLinkId != null)
            {
                var groupInDb = await queueContext.GroupEntity.FirstOrDefaultAsync(ge => ge.Id == target.GroupLinkId, cancellationToken);
                if (groupInDb == null)
                {
                    throw new ApiException(ExceptionEvents.GroupParentIdNotExists);
                }
            }
            var newTarget = target.FromModelToEntity();
            queueContext.TargetEntity.Add(newTarget);
            await queueContext.SaveChangesAsync(cancellationToken);
            return newTarget.Id;
        }
    }
}
