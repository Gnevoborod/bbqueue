using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class QueueContext:DbContext
    {
        public DbSet<Window> Window { get; set; }
        public DbSet<WindowTarget> WindowTarget { get; set; }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketAmount> TicketAmount { get; set; }
        public DbSet<TicketOperation> TicketOperation { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Target> Target { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public QueueContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bbqueue;Username=postgres;Password=qwerty");
        }
    }
}
