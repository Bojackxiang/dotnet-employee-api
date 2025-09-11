using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Services;
using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Controllers
{
  [ApiController]
  [Route("api/employee")]
  public class EmployeeController : Controller
  {

    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
      _employeeService = employeeService;
    }
    // route
    // - [GET]
    // - /api/employee
    [HttpGet]
    public IActionResult GetAll()
    {
      return Ok(_employeeService.GetAll());
    }

    // route
    // - [POST]
    // - /api/employee/new
    [HttpPost]
    public IActionResult Add([FromBody] EmployeeDto employee)
    {
      if (!ModelState.IsValid) { return BadRequest(ModelState); }
      var updated = _employeeService.Add(employee);

      return StatusCode(201, new { id = updated.Id });
    }

    // route
    // - [GET]
    // - /api/employee/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var employee = _employeeService.GetById(id);
      if (employee == null)
      {
        return NotFound(new { message = "Employee is not found" });
      }

      return StatusCode(200, employee);
    }

    // route
    // - [GET]
    // - /api/employee/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
      _employeeService.Delete(id);

      return Ok(new { success = true });
    }

    // route
    // - [UPDATE]
    // - /api/employee/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateById(int id, [FromBody] EmployeeDto employee)
    {
      var existedEmployee = _employeeService.GetById(id);

      if (existedEmployee == null)
      {
        return NotFound(new { message = "Employee is not found" });
      }

      _employeeService.Update(id, employee);

      return Ok(new { success = true });
    }
  }
}
