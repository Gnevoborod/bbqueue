namespace EmployeeService.Infrastructure.Middleware
{
    public class RequestIdentityMiddleware
    {
        private readonly RequestDelegate next;

        public RequestIdentityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string requestId = httpContext.TraceIdentifier;
            httpContext.Response.Headers.Add("RequestId", requestId);
            await next.Invoke(httpContext);
        }
    }
}
