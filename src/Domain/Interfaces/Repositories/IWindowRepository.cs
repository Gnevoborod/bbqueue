using bbqueue.Domain.Models;

namespace bbqueue.Domain.Interfaces.Repositories
{
    public interface IWindowRepository
    {
        Task ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken);
        Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken);
    }
}