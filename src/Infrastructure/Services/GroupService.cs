using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class GroupService
    {
        public List<Group> GetGroups()
        {
            return new GroupRepository().GetGroups();
        }
    }
}
