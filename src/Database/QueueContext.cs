﻿using bbqueue.Database.Entities;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
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
        private static string? connectionString;

        private void SetConnectionString(bool IntegrationTests = false)
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();
            connectionString = IntegrationTests?
                  config.GetConnectionString("DatabaseConnsectionStringForIntegrationTests") ?? throw new NullReferenceException("Невозможно получить путь к базе") 
                : config.GetConnectionString("DatabaseConnectionString") ?? throw new NullReferenceException("Невозможно получить путь к базе");
        }

        public QueueContext() 
        {
            if (connectionString == null)
                SetConnectionString();
            Database.EnsureCreated();
        }
        public QueueContext(DbContextOptions options):base(options)
        {
            if (connectionString == null)
                SetConnectionString();
            Database.EnsureCreated();
        }
        
        public QueueContext(bool IntegrationTests)
        {
            if (connectionString == null)
                SetConnectionString(IntegrationTests);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (String.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException("connectionString");
                optionsBuilder.UseNpgsql(connectionString)
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                                builder => builder.AddNLogWeb()
                                )
                        );
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bbqueue;Username=postgres;Password=qwerty");
        }
    }
}
