using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using System.Threading;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        public Task AddEmployeeAsync(EmployeeEntity employeeEntity, CancellationToken cancellationToken);
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);

        public Task AddEmployeeToWindowAsync(long employeeEntityId, long windowEntityId, CancellationToken cancellationToken);

        public Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);
        public Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);
    }
}
