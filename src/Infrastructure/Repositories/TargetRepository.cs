using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
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
    }
}
