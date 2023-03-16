using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IWindowRepository
    {
        Task ChangeWindowWorkStateAsync(Window window,  long employeeId, CancellationToken cancellationToken);
        Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);
        public Task<Window> GetWindowByEmployeeId(long employeeId, CancellationToken cancellationToken);
    }
}