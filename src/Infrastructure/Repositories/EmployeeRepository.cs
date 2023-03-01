using bbqueue.Database;
using bbqueue.Database.Entities;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;

namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class EmployeeRepository : IEmployeeRepository
    {
        IServiceProvider serviceProvider;
        public EmployeeRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<bool> AddEmployeeAsync(EmployeeEntity employeeEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }
        public async Task<bool> SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }
        public async Task<bool> AddEmployeeToWindowAsync(EmployeeEntity employeeEntity, WindowEntity windowEntity, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }

        public async Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken)
        {
            var employee = await serviceProvider
                        .GetService<QueueContext>()?
                        .EmployeeEntity?
                        .SingleOrDefaultAsync(e => e.ExternalSystemIdentity == externalNumber)!;
            return employee.FromEntityToModel()!;
        }
        public async Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
			var employee = await serviceProvider
                        .GetService<QueueContext>()?
                        .EmployeeEntity?
                        .SingleOrDefaultAsync(e => e.Id == employeeId)!;
            return employee.FromEntityToModel()!;
        }

        public async Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return new();
        }
    }
}
