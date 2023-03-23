using bbqueue.Infrastructure.Exceptions;
using System.Security.Claims;

namespace bbqueue.Infrastructure.Extensions
{
    public static class ClaimsExtension
    {


        public static long GetUserId(this ClaimsPrincipal claims)
        {
            var employeeId = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value;
            if (employeeId == null)
                throw new ApiException(ExceptionEvents.UserIdUndefined);
            return Int64.Parse(employeeId);
        }
    }
}
