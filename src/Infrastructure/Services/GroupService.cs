using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;

namespace bbqueue.Infrastructure.Services
{
    public sealed class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            return groupRepository.GetGroupsAsync(cancellationToken);
        }

        public Task<long> AddGroupAsync(Group group) 
        {
            return groupRepository.AddGroupAsync(group);
        }
    }
}
