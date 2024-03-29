﻿using EmployeeService.Domain.Interfaces.Repositories;
using EmployeeService.Domain.Interfaces.Services;
using EmployeeService.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeService.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEmployeeService employeeService;
        public AuthorizationService(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public async Task<string?> GetJwtAsync(string employeeId, CancellationToken cancellationToken)
        {
            var employee = await employeeService.GetEmployeeInfoAsync(employeeId, cancellationToken)!;
            if (employee == null)
            {
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
