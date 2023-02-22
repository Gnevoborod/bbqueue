using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class TargetRepository : ITargetRepository
    {
        IServiceProvider serviceProvider;
        public TargetRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken)
        {
            var queueContext = serviceProvider.GetService<QueueContext>();
            return await queueContext?
                .TargetEntity?
                .OrderByDescending(g => g.GroupLinkId)
                .Select(t => t.FromEntityToModel()!)
                .ToListAsync()!;
        }
    }
}
