using EmployeeService.Controllers.Dtos.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EmployeeService.Infrastructure.Exceptions
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnException(ExceptionContext exceptionContext)
        {
            var errorDto = new ErrorDto()
            {
                Code = exceptionContext.Exception.HResult.ToString(),
                Message = exceptionContext.Exception.Message,
            };
            exceptionContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            exceptionContext.Result = new JsonResult(errorDto);
            logger.LogError(exceptionContext.Exception.HResult, exceptionContext.Exception.Message);
        }

    }
}
