using bbqueue.Database.Entities;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace bbqueue.Database
{
    internal sealed class QueueContext : DbContext
    {
        public DbSet<WindowEntity>? WindowEntity { get; set; }
        public DbSet<WindowTargetEntity>? WindowTargetEntity { get; set; }

        public DbSet<TicketEntity>? TicketEntity { get; set; }
        public DbSet<TicketAmountEntity>? TicketAmountEntity { get; set; }
        public DbSet<TicketOperationEntity>? TicketOperationEntity { get; set; }
        public DbSet<GroupEntity>? GroupEntity { get; set; }
        public DbSet<TargetEntity>? TargetEntity { get; set; }

        public DbSet<EmployeeEntity>? EmployeeEntity { get; set; }
        private readonly string connectionString;
        public QueueContext(string connectionString)
        {
            this.connectionString = connectionString;
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (String.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException("connectionString");
                optionsBuilder.UseNpgsql(connectionString);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bbqueue;Username=postgres;Password=qwerty");
        }
    }
}
