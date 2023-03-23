using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace bbqueue.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeService employeeService;
        private readonly ILogger<AuthorizationService> logger;
        public AuthorizationService(IEmployeeRepository employeeRepository, IEmployeeService employeeService, ILogger<AuthorizationService> logger)
        {
            this.employeeRepository = employeeRepository;
            this.employeeService = employeeService;
            this.logger = logger;
        }

        public async Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken)
        {
            var employee = await employeeService.GetEmployeeInfoAsync(employeeId, cancellationToken)!;
            if (employee == null)
            {
                logger.LogError(ExceptionEvents.EmployeeNotFound, ExceptionEvents.EmployeeNotFound.Name + $". Employee external identity = {employeeId}");
                throw new ApiException(ExceptionEvents.EmployeeNotFound);
            }

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Role,employee.Role.ToString()),
               new Claim(ClaimTypes.PrimarySid, employee.Id.ToString())
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
