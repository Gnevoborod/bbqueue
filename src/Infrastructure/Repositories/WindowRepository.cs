using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class WindowRepository : IWindowRepository
    {
        private readonly QueueContext queueContext;
        private readonly ILogger<WindowRepository> logger;
        public WindowRepository(QueueContext queueContext, ILogger<WindowRepository> logger)
        {
            this.queueContext = queueContext;
            this.logger = logger;
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
                logger.LogError(ExceptionEvents.WindowNotExists, ExceptionEvents.WindowNotExists.Name + $". Window number = {window.Number}, employeeId = {employeeId}");
                throw new ApiException(ExceptionEvents.WindowNotExists);
            }
            if (windowEntity.EmployeeId != employeeId)
            {
                logger.LogError(ExceptionEvents.WindowRelatedToAnotherEmployee, ExceptionEvents.WindowRelatedToAnotherEmployee.Name + $". Window number = {window.Number}, employeeId из запроса = {employeeId}, employeeId к которому привязано окно {windowEntity.EmployeeId}");
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
    }
}
