using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Services;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Extensions;

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
      var employees = _employeeService.GetAll();
      return this.ApiSuccess(employees, "成功获取所有员工信息");
    }

    // route
    // - [POST]
    // - /api/employee
    [HttpPost]
    public IActionResult Add([FromBody] EmployeeDto employee)
    {
      if (!ModelState.IsValid)
      {
        return this.ApiValidationError(ModelState, "员工信息验证失败");
      }

      var newEmployee = _employeeService.Add(employee);
      return this.ApiSuccess(new { id = newEmployee.Id }, "员工创建成功");
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
        return this.ApiError("员工不存在", 404);
      }

      return this.ApiSuccess(employee, "成功获取员工信息");
    }

    // route
    // - [DELETE]
    // - /api/employee/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
      var isDeleted = _employeeService.Delete(id);

      if (!isDeleted)
      {
        return this.ApiError("员工不存在或删除失败", 404);
      }

      return this.ApiSuccess("员工删除成功");
    }

    // route
    // - [PUT]
    // - /api/employee/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateById(int id, [FromBody] EmployeeDto employee)
    {
      if (!ModelState.IsValid)
      {
        return this.ApiValidationError(ModelState, "员工信息验证失败");
      }

      var existedEmployee = _employeeService.GetById(id);
      if (existedEmployee == null)
      {
        return this.ApiError("员工不存在", 404);
      }

      var updatedEmployee = _employeeService.Update(id, employee);
      return this.ApiSuccess(updatedEmployee, "员工信息更新成功");
    }
  }
}
