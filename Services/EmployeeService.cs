using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Data;

namespace EmployeeManagementApi.Services
{
  public class EmployeeService
  {
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
      return await _context.Employees
          .OrderBy(e => e.CreatedAt)
          .ToListAsync();
    }

    public IEnumerable<Employee> GetAll()
    {
      return _context.Employees
          .OrderBy(e => e.CreatedAt)
          .ToList();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
      return await _context.Employees
          .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Employee? GetById(int id)
    {
      return _context.Employees
          .FirstOrDefault(e => e.Id == id);
    }

    public async Task<Employee> AddAsync(EmployeeDto dto)
    {
      var employee = new Employee
      {
        Name = dto.Name,
        Email = dto.Email,
        Department = dto.Department,
        HireDate = dto.HireDate,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };

      _context.Employees.Add(employee);
      await _context.SaveChangesAsync();
      return employee;
    }

    public Employee Add(EmployeeDto dto)
    {
      var employee = new Employee
      {
        Name = dto.Name,
        Email = dto.Email,
        Department = dto.Department,
        HireDate = dto.HireDate,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };

      _context.Employees.Add(employee);
      _context.SaveChanges();
      return employee;
    }

    public async Task<bool> UpdateAsync(int id, EmployeeDto dto)
    {
      var employee = await GetByIdAsync(id);
      if (employee == null) return false;

      employee.Name = dto.Name;
      employee.Email = dto.Email;
      employee.Department = dto.Department;
      employee.HireDate = dto.HireDate;
      employee.UpdatedAt = DateTime.UtcNow;

      await _context.SaveChangesAsync();
      return true;
    }

    public bool Update(int id, EmployeeDto dto)
    {
      var employee = GetById(id);
      if (employee == null) return false;

      employee.Name = dto.Name;
      employee.Email = dto.Email;
      employee.Department = dto.Department;
      employee.HireDate = dto.HireDate;
      employee.UpdatedAt = DateTime.UtcNow;

      _context.SaveChanges();
      return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var employee = await GetByIdAsync(id);
      if (employee == null) return false;

      _context.Employees.Remove(employee);
      await _context.SaveChangesAsync();
      return true;
    }

    public bool Delete(int id)
    {
      var employee = GetById(id);
      if (employee == null) return false;

      _context.Employees.Remove(employee);
      _context.SaveChanges();
      return true;
    }
  }
}
