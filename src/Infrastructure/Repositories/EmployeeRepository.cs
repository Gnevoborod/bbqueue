using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly QueueContext queueContext;
        public EmployeeRepository(QueueContext queueContext)
        {
            this.queueContext = queueContext;
        }
        public async Task AddEmployeeAsync(EmployeeEntity employeeEntity, CancellationToken cancellationToken)
        {
            queueContext.EmployeeEntity.Add(employeeEntity);
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public async Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            var employee = await queueContext.EmployeeEntity.SingleOrDefaultAsync(e=>e.Id==employeeId, cancellationToken);
            if (employee == null)
                throw new Exception("Пользователь не найден");
            employee.Role = role;
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public async Task AddEmployeeToWindowAsync(long employeeEntityId, long windowEntityId, CancellationToken cancellationToken)
        {
            var window = await queueContext.WindowEntity.SingleOrDefaultAsync(w => w.Id == windowEntityId, cancellationToken);
            if (window == null)
                throw new Exception("Не найдено указанного окна");
            //нашли окно - ищем есть ли другие окна с этим сотрудником. Один сотрудник - одно окно
            var oldWindow = await queueContext.WindowEntity.SingleOrDefaultAsync(w => w.EmployeeId == employeeEntityId, cancellationToken);
            if(oldWindow!=null)
            {
                oldWindow.EmployeeId = null;
            }
            window.EmployeeId = employeeEntityId;
            await queueContext.SaveChangesAsync(cancellationToken);

        }

        public async Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken)
        {
            var employee = await queueContext
                        .EmployeeEntity
                        .SingleOrDefaultAsync(e => e.ExternalSystemIdentity == externalNumber, cancellationToken);
            return employee == null ? default! : employee.FromEntityToModel();
        }
        public async Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
			var employee = await queueContext
                        .EmployeeEntity
                        .SingleOrDefaultAsync(e => e.Id == employeeId, cancellationToken);
            return employee == null? default! : employee.FromEntityToModel();
        }

        public async Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return new();
        }
    }
}
