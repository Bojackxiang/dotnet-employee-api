using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Data
{
  /// <summary>
  /// Employee Management API 数据库上下文
  /// </summary>
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet 属性 - 表示数据库中的表
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // 配置 Employee 实体
      modelBuilder.Entity<Employee>(entity =>
      {
        // 设置主键
        entity.HasKey(e => e.Id);

        // 配置字段属性
        entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

        entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(200);

        entity.Property(e => e.Department)
                  .HasMaxLength(100);

        // 设置默认值
        entity.Property(e => e.HireDate)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.UpdatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // 创建索引
        entity.HasIndex(e => e.Email)
                  .IsUnique()
                  .HasDatabaseName("IX_employees_email");
      });

      // 可以在这里添加种子数据
      SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
      // 使用固定的静态日期时间值而不是动态的 DateTime.UtcNow
      var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      var hireDate1 = new DateTime(2023, 1, 15, 0, 0, 0, DateTimeKind.Utc);
      var hireDate2 = new DateTime(2023, 6, 20, 0, 0, 0, DateTimeKind.Utc);

      modelBuilder.Entity<Employee>().HasData(
          new Employee
          {
            Id = 1,
            Name = "张三",
            Email = "zhangsan@example.com",
            Department = "IT部门",
            HireDate = hireDate1,
            CreatedAt = fixedDate,
            UpdatedAt = fixedDate
          },
          new Employee
          {
            Id = 2,
            Name = "李四",
            Email = "lisi@example.com",
            Department = "人事部门",
            HireDate = hireDate2,
            CreatedAt = fixedDate,
            UpdatedAt = fixedDate
          }
      );
    }
  }
}
