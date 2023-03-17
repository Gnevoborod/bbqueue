using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;

namespace bbqueue.Infrastructure.Services
{
    public sealed class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
           return employeeRepository.AddEmployeeAsync(employee.FromModelToEntity(), cancellationToken);
        }
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            return employeeRepository.SetRoleToEmployeeAsync(employeeId, role, cancellationToken); 
        }

        public Task AddEmployeeToWindowAsync(long employeeId, long windowId, CancellationToken cancellationToken)
        {
            return employeeRepository.AddEmployeeToWindowAsync(employeeId, windowId, cancellationToken);
        }

        public Task<Employee?> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken)
        {
            return employeeRepository
                .GetEmployeeInfoAsync(externalNumber, cancellationToken)!;
        }
        public Task<Employee?> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
            return employeeRepository
                .GetEmployeeInfoAsync(employeeId, cancellationToken)!;
        }

        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            return employeeRepository.GetEmployeeListAsync(cancellationToken);
        }

    }
}
