using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using System.Diagnostics;

namespace bbqueue.Infrastructure.Services
{
    public sealed class TargetService : ITargetService
    {
        private readonly IGroupRepository groupRepository;
        private readonly ITargetRepository targetRepository;

        public TargetService(IGroupRepository groupRepository, ITargetRepository targetRepository)
        {
            this.groupRepository = groupRepository;
            this.targetRepository = targetRepository;
        }
        public Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken)
        {
            return targetRepository.GetTargetsAsync(cancellationToken);
        }


        public async Task<GroupHierarchyDto> GetHierarchyAsync(CancellationToken cancellationToken)
        {
            var Groups = await groupRepository.GetGroupsAsync(cancellationToken);
            var Targets = await GetTargetsAsync(cancellationToken);
            var result= Groups.FromModelToHierarchyDto(Targets);
            return result;
        }
    }
}
