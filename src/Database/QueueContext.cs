using bbqueue.Database.Entities;
using bbqueue.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

namespace bbqueue.Database
{
    public sealed class QueueContext : DbContext
    {
        public DbSet<WindowEntity> WindowEntity { get; set; } = default!;

        public DbSet<TicketEntity> TicketEntity { get; set; } = default!;
        public DbSet<TicketAmountEntity> TicketAmountEntity { get; set; } = default!;
        public DbSet<TicketOperationEntity> TicketOperationEntity { get; set; } = default!;
        public DbSet<GroupEntity> GroupEntity { get; set; } = default!;
        public DbSet<TargetEntity> TargetEntity { get; set; } = default!;
        public DbSet<WindowTargetEntity> WindowTargetEntity { get; set; } = default!;
        public DbSet<EmployeeEntity> EmployeeEntity { get; set; } = default!;
        private static string? connectionString;

        private void SetConnectionString()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("DatabaseConnectionString") ?? throw new NullReferenceException("Невозможно получить путь к базе");
        }

        public QueueContext() 
        {
            if (connectionString == null)
                SetConnectionString();
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketAmountEntity>().HasIndex(tae => tae.Prefix).IsUnique(true);
            modelBuilder.Entity<TargetEntity>().HasIndex(te => te.Prefix).IsUnique(true);
        }
    }
}
