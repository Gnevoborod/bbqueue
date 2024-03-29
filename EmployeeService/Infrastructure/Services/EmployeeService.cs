﻿using EmployeeService.Domain.Interfaces.Repositories;
using EmployeeService.Domain.Interfaces.Services;
using EmployeeService.Domain.Models;
using EmployeeService.Mapper;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace EmployeeService.Infrastructure.Services
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
           return employeeRepository.AddEmployeeAsync(employee, cancellationToken);
        }
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            return employeeRepository.SetRoleToEmployeeAsync(employeeId, role, cancellationToken); 
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
