﻿using bbqueue.Database.Entities;
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
                throw new Exception("Не удалось найти окно по номеру"); //Тут нужен свой эксепшн
            if (windowEntity.EmployeeId != employeeId)
                throw new Exception("Невозможно изменить состояние окна, так как к данному окну привязан другой пользователь.");
            windowEntity.WindowWorkState = window.WindowWorkState;
            await queueContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Window> GetWindowByEmployeeId(long employeeId, CancellationToken cancellationToken)
        {
            var window = await queueContext.WindowEntity.SingleOrDefaultAsync(w=>w.EmployeeId==employeeId, cancellationToken);
            return window == null ? default! : window.FromEntityToModel();
        }
    }
}
