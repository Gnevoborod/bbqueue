using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface IEmployeeService
    {
        public Task<bool> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        public Task<bool> SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);

        public Task<bool> AddEmployeeToWindowAsync(Employee employee, Window window, CancellationToken cancellationToken);

        public Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);
        public Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);

        public Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken);
    }
}
