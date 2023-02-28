using bbqueue.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using bbqueue.Controllers.Dtos;
using System.Security.Claims;
using System;
using bbqueue.Controllers.Dtos.Employee;

namespace bbqueue.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        [HttpGet("jwt")]
        public async Task<IActionResult> GetJWT([FromQuery] string employeeExternalId)
        {
            //пока метод-заглушка, чисто получить jwt и с ним дальше работать
            //тут вот будем асинхронно получать данные по пользователю и т.д.
            await Task.Run(() => Thread.Sleep(10));



            var claims = new List<Claim> {
               new Claim(ClaimTypes.Role,"employee"),
               new Claim(ClaimTypes.PrimarySid,"1")//сюда надо будет пробрасывать идентификатор пользователя
            };
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            JwtDto result = new()
            {
                Token = encodedJwt
            };
            return Ok(result);
        }
    }
}
