using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    internal interface ITargetRepository
    {
        Task<List<Target>>? GetTargetsAsync(CancellationToken cancellationToken);
    }
}