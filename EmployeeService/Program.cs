using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using NLog;
using NLog.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EmployeeService.Database;
using EmployeeService.Infrastructure;
using EmployeeService.Infrastructure.Services;
using EmployeeService.Infrastructure.Repositories;
using EmployeeService.Domain.Interfaces.Services;
using EmployeeService.Domain.Interfaces.Repositories;

using EmplService = EmployeeService.Infrastructure.Services.EmployeeService;
using EmployeeService.Infrastructure.Exceptions;
using EmployeeService.Infrastructure.Middleware;

namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
     
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            //builder.Logging.AddEventLog();

            //builder.Services.AddScoped<QueueContext>();
            if (builder.Environment.IsProduction())
                builder.WebHost.UseUrls("http://*:5015");
            builder.Services.AddMvc(options => options.Filters.Add(typeof(ApiExceptionFilter)));
            builder.Services.AddDbContext<QueueContext>(ServiceLifetime.Scoped);
            builder.Services.AddScoped<IEmployeeService, EmplService>();
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
                    Title = "BBQueue API. Сервис управления сотрудниками.",
                    Description = "BBQueue - очередь лучшей прожарки"
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


            });

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<RequestIdentityMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}