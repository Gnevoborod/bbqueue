using bbqueue.Domain.Interfaces.Services;

using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;

namespace bbqueue.Infrastructure.Services
{
    public sealed class WindowService : IWindowService
    {
        private readonly IWindowRepository windowRepository;
        public WindowService(IWindowRepository windowRepository)
        {
            this.windowRepository = windowRepository;
        }
        public Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            return windowRepository.GetWindowsAsync(cancellationToken);
        }

        public Task ChangeWindowWorkStateAsync(Window window, long employeeId, CancellationToken cancellationToken)
        {
            return windowRepository.ChangeWindowWorkStateAsync(window,employeeId, cancellationToken);
        }

        public Task<long> AddNewWindowAsync(Window window, CancellationToken cancellationToken)
        {
            return windowRepository.AddNewWindowAsync(window, cancellationToken);
        }

        public Task AddTargetToWindowAsync(long WindowId, long TargetId, CancellationToken cancellationToken)
        {
            return windowRepository.AddTargetToWindowAsync(WindowId, TargetId, cancellationToken);
        }

        public Task AddEmployeeToWindowAsync(long employeeId, long windowId, CancellationToken cancellationToken)
        {
            return windowRepository.AddEmployeeToWindowAsync(employeeId, windowId, cancellationToken);
        }
    }
}
