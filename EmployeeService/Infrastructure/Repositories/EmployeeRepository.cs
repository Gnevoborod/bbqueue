using EmployeeService.Controllers;
using EmployeeService.Database;
using EmployeeService.Domain.Interfaces.Repositories;
using EmployeeService.Domain.Models;
using EmployeeService.Infrastructure.Exceptions;
using EmployeeService.Mapper;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
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
            {
                throw new ApiException(ExceptionEvents.EmployeeNotFound);
            }
            employee.Role = role;
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
