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

        /// <summary>
        /// Поставляет ID пользователя из JWT токена
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public long GetUserId(HttpContext httpContext);
    }
}