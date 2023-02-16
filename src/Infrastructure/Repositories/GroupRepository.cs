using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class GroupRepository
    {
        public List<Group> GetGroups()
        {
            using (QueueContext queueContext = new QueueContext())
            {
                var groupEntity = queueContext?.GroupEntity?.OrderByDescending(g=>g.GroupLinkId);
                if (groupEntity == null)
                    return new List<Group>();
                List<Group> groups = new List<Group>(groupEntity.Count());
                foreach (GroupEntity ge in groupEntity)
                {
                    groups.Add(ge.FromEntityToModel()!);
                }
                return groups;
            }
        }
    }
}
