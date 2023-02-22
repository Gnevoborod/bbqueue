using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Services
{
    internal interface IWindowService
    {
        Task<bool> ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken);
        Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);
        bool SetEmployeeToWindowAsync(int employeeId, int windowId, CancellationToken cancellationToken);
    }
}