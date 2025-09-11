using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Services
{
  public class EmployeeService
  {
    private readonly List<Employee> _employees = new();
    private int _nextId = 1;

    public IEnumerable<Employee> GetAll() => _employees;

    public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public Employee Add(EmployeeDto dto)
    {
      var employee = new Employee
      {
        Id = _nextId++,
        Name = dto.Name,
        Email = dto.Email,
        Department = dto.Department,
        HireDate = dto.HireDate
      };
      _employees.Add(employee);
      return employee;
    }

    public bool Update(int id, EmployeeDto dto)
    {
      var emp = GetById(id);
      if (emp == null) return false;
      emp.Name = dto.Name;
      emp.Email = dto.Email;
      emp.Department = dto.Department;
      emp.HireDate = dto.HireDate;
      return true;
    }

    public bool Delete(int id)
    {
      var emp = GetById(id);
      if (emp == null) return false;
      _employees.Remove(emp);
      return true;
    }
  }
}
