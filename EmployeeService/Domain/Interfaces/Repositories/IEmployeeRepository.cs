using EmployeeService.Database.Entities;
using EmployeeService.Domain.Models;
using System.Threading;

namespace EmployeeService.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Сохраняет нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        /// <summary>
        /// Устанавливает роль для сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет инфо о сотруднике
        /// </summary>
        /// <param name="externalNumber">Внешний идентификатор</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет инфо о сотруднике
        /// </summary>
        /// <param name="employeeId">Внутренний идентификатор</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);
        /// <summary>
        /// Поставляет список сотрудников
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);
    }
}
