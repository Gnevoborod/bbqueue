using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IWindowService
    {
        public Task ChangeWindowWorkStateAsync(Window window, long employeeId, CancellationToken cancellationToken);
        public Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);
    }
}