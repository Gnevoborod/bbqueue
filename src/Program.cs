using bbqueue.Database;
using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure;
using bbqueue.Infrastructure.Repositories;
using bbqueue.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using NLog;
using NLog.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using bbqueue.Infrastructure.Middleware;
using bbqueue.Infrastructure.Exceptions;
using bbqueue.Infrastructure.Jobs;

namespace bbqueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            try
            {
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                //builder.Logging.AddEventLog();

                //builder.Services.AddScoped<QueueContext>();
                if(builder.Environment.IsProduction())
                    builder.WebHost.UseUrls("http://*:5005");
                builder.Services.AddMvc(options => options.Filters.Add(typeof(ApiExceptionFilter)));
                builder.Services.AddHostedService<TicketsCleanHostedService>();
                builder.Services.AddDbContext<QueueContext>(ServiceLifetime.Scoped);
                builder.Services.AddScoped<IGroupRepository, GroupRepository>();
                builder.Services.AddScoped<IGroupService, GroupService>();

                builder.Services.AddScoped<ITargetRepository, TargetRepository>();
                builder.Services.AddScoped<ITargetService, TargetService>();

                builder.Services.AddScoped<IWindowRepository, WindowRepository>();
                builder.Services.AddScoped<IWindowService, WindowService>();

                builder.Services.AddScoped<ITicketRepository, TicketRepository>();
                builder.Services.AddScoped<ITicketService, TicketService>();

                builder.Services.AddScoped<IQueueService, QueueService>();

                builder.Services.AddScoped<IEmployeeService, EmployeeService>();
                builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

                builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

                builder.Services.AddAuthorization();
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(
                    options =>
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

                builder.Services.AddSwaggerGen(options =>
                {
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = "Put **_ONLY_** JWT Bearer token on textbox below!",

                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                    });


                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "BBQueue API",
                        Description = "BBQueue - очередь лучшей прожарки"
                    });

                    // using System.Reflection;
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


                });

                

                var app = builder.Build();

                // Configure the HTTP request pipeline.
               // if (app.Environment.IsDevelopment())
                //{
                    app.UseSwagger();
                    app.UseSwaggerUI();
                //}

                app.UseMiddleware<RequestIdentityMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                
                
                app.MapControllers();
                app.Run();
            }
            catch(Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}