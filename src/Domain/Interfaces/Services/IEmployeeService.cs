using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);

        public Task AddEmployeeToWindowAsync(long employeeId, long windowId, CancellationToken cancellationToken);

        public Task<Employee?> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);
        public Task<Employee?> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);

    }
}
