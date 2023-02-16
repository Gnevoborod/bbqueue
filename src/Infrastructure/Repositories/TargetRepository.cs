using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class TargetRepository
    {
        public List<Target> GetTargets()
        {
            using (QueueContext queueContext = new QueueContext())
            {
                var targetEntity = queueContext?.TargetEntity?.OrderByDescending(g => g.GroupLinkId);
                if (targetEntity == null)
                    return new List<Target>();
                List<Target> targets = new List<Target>(targetEntity.Count());
                foreach(TargetEntity te in targetEntity)
                {
                    targets.Add(te.FromEntityToModel()!);
                }
                return targets;
            }
        }
    }
}
