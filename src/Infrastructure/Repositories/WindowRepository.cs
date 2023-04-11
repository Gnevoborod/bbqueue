using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using bbqueue.Database.Entities;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class WindowRepository : IWindowRepository
    {
        private readonly QueueContext queueContext;
        public WindowRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            return queueContext.WindowEntity.OrderBy(w => w.Number)
                .Select(w => w.FromEntityToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task ChangeWindowWorkStateAsync(Window window, long employeeId, CancellationToken cancellationToken)
        {
            var windowEntity = await queueContext
                                    .WindowEntity
                                    .SingleOrDefaultAsync(we => we.Number == window.Number);
            if (windowEntity == null)
            {
                throw new ApiException(ExceptionEvents.WindowNotExists);
            }
            if (windowEntity.EmployeeId != employeeId)
            {
                throw new ApiException(ExceptionEvents.WindowRelatedToAnotherEmployee);
            }
            windowEntity.WindowWorkState = window.WindowWorkState;
            if(windowEntity.WindowWorkState == WindowWorkState.Closed)
            {
                windowEntity.EmployeeId = null;
            }
            await queueContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Window> GetWindowByEmployeeId(long employeeId, CancellationToken cancellationToken)
        {
            var window = await queueContext.WindowEntity.SingleOrDefaultAsync(w=>w.EmployeeId==employeeId, cancellationToken);
            return window == null ? default! : window.FromEntityToModel();
        }

        public async Task<long> AddNewWindowAsync(Window window, CancellationToken cancellationToken)
        {
            var windowInDb = await queueContext.WindowEntity.FirstOrDefaultAsync(w=>w.Number== window.Number, cancellationToken);
            if (windowInDb != null)
                throw new ApiException(ExceptionEvents.WindowNumberExists);
            var newWindow = window.FromModelToEntity();
            queueContext.WindowEntity.Add(newWindow);
            await queueContext.SaveChangesAsync(cancellationToken);
            return newWindow.Id;
        }

 
        public async Task AddTargetToWindowAsync(long WindowId, long TargetId, CancellationToken cancellationToken)
        {
            var window = await queueContext.WindowEntity.FirstOrDefaultAsync(w => w.Id == WindowId);
            if (window == null)
                throw new ApiException(ExceptionEvents.WindowNotExists);
            var target = await queueContext.TargetEntity.FirstOrDefaultAsync(t=>t.Id == TargetId);
            if(target == null)
                throw new ApiException(ExceptionEvents.TargetNotExists);
            var windowTargetId = await queueContext.WindowTargetEntity.FirstOrDefaultAsync(wte => wte.WindowId == WindowId && wte.TargetId == TargetId, cancellationToken);
            if (windowTargetId != null)
                throw new ApiException(ExceptionEvents.WindowTargetExists);

            WindowTargetEntity windowTarget = new ()
            {
                WindowId = WindowId,
                TargetId = TargetId
            };

            queueContext.WindowTargetEntity.Add(windowTarget);
            await queueContext.SaveChangesAsync(cancellationToken);

        }
    }
}
