using Microsoft.EntityFrameworkCore;
using TaskCRUD.Data;
using TaskCRUD.DTOs.Department;
using TaskCRUD.DTOs.Employee;
using TaskCRUD.Models;

namespace TaskCRUD.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetDepartmentDTO>> GetAllAsync()
        {
            return await _context.Departments
                .Select(d => new GetDepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Location = d.Location
                })
                .ToListAsync();
        }

        public async Task<GetDepartmentDTO?> GetByIdAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return null;

            return new GetDepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location
            };
        }

        public async Task<GetDepartmentWithEmployeesDTO?> GetWithEmployeesAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return null;

            return new GetDepartmentWithEmployeesDTO
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location,
                Employees = department.Employees.Select(e => new GetEmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Age = e.Age,
                    Salary = e.Salary,
                    HireDate = e.HireDate,
                    JobLevel = e.JobLevel,
                    Status = e.Status,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = department.Name
                }).ToList()
            };
        }

        public async Task<GetDepartmentDTO> CreateAsync(CreateDepartmentDTO dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                Location = dto.Location
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return new GetDepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateDepartmentDTO dto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            department.Name = dto.Name;
            department.Location = dto.Location;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, PatchDepartmentDTO dto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            if (dto.Name != null) department.Name = dto.Name;
            if (dto.Location != null) department.Location = dto.Location;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }
    }
}
