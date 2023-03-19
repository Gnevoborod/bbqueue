using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Добавляет сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        /// <summary>
        /// Привязывает роль к сотруднику
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken);
        /// <summary>
        /// Привязывает сотрудника к окну
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="windowId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public Task AddEmployeeToWindowAsync(long employeeId, long windowId, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет данные сотрудника по внешнему идентификатору
        /// </summary>
        /// <param name="externalNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Employee?> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет данные сотрудника по внутреннему идентификатору
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Employee?> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken);

        /// <summary>
        /// Поставляет список сотрудников
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken);

    }
}
