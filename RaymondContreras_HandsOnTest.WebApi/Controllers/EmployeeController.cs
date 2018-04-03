using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaymondContreras_HandsOnTest.Services.Interfaces;

namespace RaymondContreras_HandsOnTest.WebApi.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get All employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            return new ObjectResult(
                await Task.Run(() => _employeeService.GetAllEmployee()));
        }

        /// <summary>
        /// Get by employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return new ObjectResult(
                await Task.Run(() => _employeeService.GetAllEmployeeById(id)));
        }
    }
}