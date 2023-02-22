using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface ITargetService
    {
        Task<GroupHierarchyDto>? GetHierarchyAsync(CancellationToken cancellationToken);
        Task<List<Target>>? GetTargetsAsync(CancellationToken cancellationToken);
    }
}