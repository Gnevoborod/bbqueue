using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using bbqueue.Domain.Interfaces.Repositories;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class WindowRepository : IWindowRepository
    {
        private readonly QueueContext queueContext;
        public WindowRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public async Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            return await queueContext.WindowEntity.OrderBy(w => w.Number)
                .Select(w => w.FromEntityToModel())
                .ToListAsync(cancellationToken);


        }

        public async Task ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken)
        {
            var windowEntity = await queueContext
                                    .WindowEntity
                                    .SingleOrDefaultAsync(we => we.Number == window.Number);
            if (windowEntity == null)
                throw new Exception("Не удалось найти окно по номеру"); //Тут нужен свой эксепшн
            windowEntity.WindowWorkState = window.WindowWorkState;
            await queueContext.SaveChangesAsync(cancellationToken);
        }
    }
}
