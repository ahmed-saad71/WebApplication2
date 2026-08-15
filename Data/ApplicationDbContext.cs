using TaskCRUD.Models;
using Microsoft.EntityFrameworkCore;
using TaskCRUD.Models;

namespace TaskCRUD.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}