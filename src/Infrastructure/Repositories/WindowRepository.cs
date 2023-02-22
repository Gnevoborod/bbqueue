using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using bbqueue.Domain.Interfaces.Repositories;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class WindowRepository : IWindowRepository
    {
        IServiceProvider serviceProvider;
        public WindowRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public async Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            var queueContext = serviceProvider.GetService<QueueContext>();
            cancellationToken.ThrowIfCancellationRequested();
            return await queueContext?.WindowEntity?.OrderBy(w => w.Number)
                .Select(w => w.FromEntityToModel()!)
                .ToListAsync()!;


        }

        public async Task<bool> ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken)
        {
            var queueContext = serviceProvider.GetService<QueueContext>();
            var windowEntity = await queueContext?.WindowEntity?.SingleOrDefaultAsync(we => we.Number == window.Number)!;
            if (windowEntity == null) return false;
            windowEntity.WindowWorkState = window.WindowWorkState;
            await queueContext?.SaveChangesAsync(cancellationToken)!;
            return true;

        }
    }
}
