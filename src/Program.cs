using bbqueue.Database;
using bbqueue.Mapper;
using System.Runtime.CompilerServices;

namespace bbqueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            var config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json").Build();
            string connectionString = config.GetConnectionString("DatabaseConnectionString") ?? "";
            //DBCONETXT SECTION
            QueueContext queueContext= new QueueContext(connectionString);
           
            // test of extensions queueContext.FromEntitiesToModels(queueContext.EmployeeEntity.ToList());
            app.Run();
        }
    }
}