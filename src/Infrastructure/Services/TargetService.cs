using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;
namespace bbqueue.Infrastructure.Services
{
    internal sealed class TargetService
    {
        public List<Target> GetTargets()
        {
            return new TargetRepository().GetTargets();
        }
    }
}
