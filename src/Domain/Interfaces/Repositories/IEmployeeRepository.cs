using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using System.Threading;

namespace bbqueue.Domain.Interfaces.Repositories
{
    internal interface IEmployeeRepository
    {
        public Task<bool> AddEmployeeAsync(EmployeeEntity employeeEntity, CancellationToken cancellationToken);
        public Task<bool> SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);

        public Task<bool> AddEmployeeToWindowAsync(EmployeeEntity employeeEntity, WindowEntity windowEntity, CancellationToken cancellationToken);

        public Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);
        public Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);
    }
}
