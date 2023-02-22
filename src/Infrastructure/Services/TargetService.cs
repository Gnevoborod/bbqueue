using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using System.Diagnostics;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class TargetService : ITargetService
    {
        private readonly IServiceProvider serviceProvider;

        public TargetService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<List<Target>>? GetTargetsAsync(CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<ITargetRepository>()?.GetTargetsAsync(cancellationToken)!;
        }


        public async Task<GroupHierarchyDto> GetHierarchyAsync(CancellationToken cancellationToken)
        {
            var Groups = await serviceProvider.GetService<IGroupRepository>()?.GetGroupsAsync(cancellationToken)!;
            var Targets = await GetTargetsAsync(cancellationToken)!;
            //тут ставим проверку на отмену потому что маппинг может занять время
            cancellationToken.ThrowIfCancellationRequested();
            var result= Groups?.FromModelToHierarchyDto(Targets)!;
            return result;
        }
    }
}
