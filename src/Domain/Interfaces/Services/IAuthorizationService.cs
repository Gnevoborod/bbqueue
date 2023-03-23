namespace bbqueue.Domain.Interfaces.Services
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// Формирует JWT токен
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken);
    }
}