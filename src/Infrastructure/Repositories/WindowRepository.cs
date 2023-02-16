using bbqueue.Database.Entities;
using bbqueue.Database;
using bbqueue.Domain.Models;
using bbqueue.Mapper;
namespace bbqueue.Infrastructure.Repositories
{
    internal sealed class WindowRepository
    {
        public List<Window> GetWindows()
        {
            using (QueueContext queueContext = new QueueContext())
            {
                var windowEntity = queueContext?.WindowEntity?.OrderBy(w=>w.Number);
                if (windowEntity == null)
                    return new List<Window>();
                List<Window> window = new List<Window>(windowEntity.Count());
                foreach (WindowEntity ge in windowEntity)
                {
                    window.Add(ge.FromEntityToModel()!);
                }
                return window;
            }
        }

        public bool ChangeWindowWorkState(Window window)
        {
            using (QueueContext queueContext = new QueueContext())
            {
                var windowEntity =queueContext?.WindowEntity?.SingleOrDefault(we=>we.Number== window.Number);
                if(windowEntity == null) return false;
                windowEntity.WindowWorkState = window.WindowWorkState;
                queueContext?.SaveChanges();
                return true;
            }
        }
    }
}
