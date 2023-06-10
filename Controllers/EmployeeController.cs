using Microsoft.AspNetCore.Mvc;
using SoftwareEngineerTest.Interface;
using SoftwareEngineerTest.Models;
using SoftwareEngineerTest.ViewModel;

namespace SoftwareEngineerTest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeServices = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeServices.GetEmployees().ToList();
            return Json(employees);
        }

        [HttpPost]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is missing.");
            }
            var success = _employeeServices.UpdateEmployee(employee);

            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeViewModel employeeVM)
        {
            bool success = _employeeServices.AddEmployee(employeeVM);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromBody] int empId)
        {
            bool success = _employeeServices.DeleteEmployee(empId);
            if(success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
