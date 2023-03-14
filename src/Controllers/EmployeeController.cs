using Microsoft.AspNetCore.Mvc;
using bbqueue.Controllers.Dtos.Employee;
using bbqueue.Domain.Interfaces.Services;

namespace bbqueue.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

    }
}
