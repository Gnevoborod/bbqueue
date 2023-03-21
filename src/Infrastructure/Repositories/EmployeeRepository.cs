using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;
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
        public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            queueContext.EmployeeEntity.Add(employee.FromModelToEntity());
            return queueContext.SaveChangesAsync(cancellationToken);
        }
        public async Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            var employee = await queueContext.EmployeeEntity.SingleOrDefaultAsync(e=>e.Id==employeeId, cancellationToken);
            if (employee == null)
                throw new ApiException(ExceptionEvents.EmployeeNotFound);
            employee.Role = role;
            await queueContext.SaveChangesAsync(cancellationToken);
        }
        public async Task AddEmployeeToWindowAsync(long employeeEntityId, long windowEntityId, CancellationToken cancellationToken)
        {
            //var window = await queueContext.WindowEntity.Include(a=>a.Employee).SingleOrDefaultAsync(w => w.Id == windowEntityId, cancellationToken);
            var window = await queueContext.WindowEntity.Where(w => w.Id == windowEntityId).Select(w=>w).SingleOrDefaultAsync(cancellationToken);
            if (window == null)
                throw new ApiException(ExceptionEvents.WindowNotExists);
            //нашли окно - ищем есть ли другие окна с этим сотрудником. Один сотрудник - одно окно
            var oldWindow = await queueContext.WindowEntity.Where(w => w.EmployeeId == employeeEntityId).Select(w => w).SingleOrDefaultAsync(cancellationToken);
            if(oldWindow!=null)
            {
                oldWindow.EmployeeId = null;
            }
            if(window.EmployeeId != null)
            {
                throw new ApiException(ExceptionEvents.WindowOccupied);
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

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            return queueContext
                    .EmployeeEntity
                    .Select(ee=>ee.FromEntityToModel())
                    .ToListAsync(cancellationToken);
        }
    }
}
