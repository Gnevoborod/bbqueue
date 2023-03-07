using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure;
using bbqueue.Infrastructure.Repositories;
using bbqueue.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            //builder.Services.AddScoped<QueueContext>();
            builder.Services.AddDbContext<QueueContext>();
            builder.Services.AddSingleton<Queue>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            
            builder.Services.AddScoped<ITargetRepository,TargetRepository>();
            builder.Services.AddScoped<ITargetService,TargetService>();

            builder.Services.AddScoped<IWindowRepository, WindowRepository>();
            builder.Services.AddScoped<IWindowService, WindowService>();

            builder.Services.AddScoped<ITicketRepository,TicketRepository>();
            builder.Services.AddScoped<ITicketService,TicketService>();

            builder.Services.AddScoped<IQueueService,QueueService>();

            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                }
                );

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
        
            app.Run();
        }
    }
}