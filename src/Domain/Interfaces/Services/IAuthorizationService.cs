namespace bbqueue.Domain.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken);
    }
}