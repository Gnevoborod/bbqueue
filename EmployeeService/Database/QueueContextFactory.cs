using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeService.Database
{
    internal sealed class QueueContextFactory: IDesignTimeDbContextFactory<QueueContext>
    {
        public QueueContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QueueContext>();
            return new QueueContext();
        }
    }
}
