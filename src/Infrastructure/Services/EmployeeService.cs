using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        public async Task<Employee?> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken)
        {
            return await employeeRepository
                .GetEmployeeInfoAsync(externalNumber, cancellationToken)!;
        }
        public async Task<Employee?> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
            return await employeeRepository
                .GetEmployeeInfoAsync(employeeId, cancellationToken)!;
        }

        public async Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return new();
        }

    }
}
