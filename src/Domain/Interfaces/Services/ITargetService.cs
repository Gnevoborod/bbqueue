using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface ITargetService
    {
        public Task<GroupHierarchyDto> GetHierarchyAsync(CancellationToken cancellationToken);
        public Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken);
    }
}