using bbqueue.Domain.Models;
namespace bbqueue.Infrastructure.Services
{
    internal sealed class WindowService
    {
        public List<Window> GetWindows()
        {
            return new List<Window>();//
        }

        public bool ChangeWindowWorkState(string windowNumber, WindowWorkState windowWorkState)
        {
            throw new NotImplementedException();
        }

        public bool SetEmployeeToWindow(int employeeId, int windowId)
        {
            throw new NotImplementedException();
        }

    }
}
