using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    public interface IWindowService
    {
        public Task ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken);
        public Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);
        bool SetEmployeeToWindowAsync(int employeeId, int windowId, CancellationToken cancellationToken);
    }
}