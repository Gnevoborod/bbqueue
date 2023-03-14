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
            await Task.Run(() => Thread.Sleep(100));
        }
        public async Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
        }
        public async Task AddEmployeeToWindowAsync(EmployeeEntity employeeEntity, WindowEntity windowEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
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
