using bbqueue.Domain.Interfaces.Services;

using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class WindowService : IWindowService
    {
        IServiceProvider serviceProvider;
        public WindowService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<List<Window>> GetWindowsAsync(CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<IWindowRepository>()?.GetWindowsAsync(cancellationToken)!;
        }

        public async Task<bool> ChangeWindowWorkStateAsync(Window window, CancellationToken cancellationToken)
        {
            return await serviceProvider.GetService<IWindowRepository>()?.ChangeWindowWorkStateAsync(window, cancellationToken)!;
        }

        public bool SetEmployeeToWindowAsync(int employeeId, int windowId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
