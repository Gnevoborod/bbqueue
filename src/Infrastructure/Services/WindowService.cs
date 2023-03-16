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
        public async Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            return await windowRepository.GetWindowsAsync(cancellationToken);
        }

        public async Task ChangeWindowWorkStateAsync(Window window, long employeeId, CancellationToken cancellationToken)
        {
            await windowRepository.ChangeWindowWorkStateAsync(window,employeeId, cancellationToken);
        }

    }
}
