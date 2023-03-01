using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace bbqueue.Infrastructure.Services
{
    internal sealed class EmployeeService: IEmployeeService
    {
        IServiceProvider serviceProvider;

        public EmployeeService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<bool> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }
        public async Task<bool> SetRoleToEmployeeAsync(long employeeId, EmployeeRole role, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }

        public async Task<bool> AddEmployeeToWindowAsync(Employee employee, Window window, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return true;
        }

        public async Task<Employee> GetEmployeeInfoAsync(string externalNumber, CancellationToken cancellationToken)
        {
            return await serviceProvider
                .GetService<IEmployeeRepository>()?
                .GetEmployeeInfoAsync(externalNumber, cancellationToken)!;
        }
        public async Task<Employee> GetEmployeeInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
            return await serviceProvider
                .GetService<IEmployeeRepository>()?
                .GetEmployeeInfoAsync(employeeId, cancellationToken)!;
        }

        public async Task<List<Employee>> GetEmployeeListAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(100));
            return new();
        }

        public async Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken)
        {
            var employee = await GetEmployeeInfoAsync(employeeId, cancellationToken)!;
            cancellationToken.ThrowIfCancellationRequested();
            if (employee == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Role,employee!.Role.ToString()),
               new Claim(ClaimTypes.PrimarySid, employee!.Id.ToString())
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}
