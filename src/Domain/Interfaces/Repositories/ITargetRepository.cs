using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface ITargetRepository
    {
        Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken);
    }
}