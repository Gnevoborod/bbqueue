using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Repositories;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class WindowService
    {
        public List<Window> GetWindows()
        {
            return new WindowRepository().GetWindows();
        }

        public bool ChangeWindowWorkState(Window window)
        {
            return new WindowRepository().ChangeWindowWorkState(window);
        }

        public bool SetEmployeeToWindow(int employeeId, int windowId)
        {
            throw new NotImplementedException();
        }

    }
}
