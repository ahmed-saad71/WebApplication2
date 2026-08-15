using Microsoft.EntityFrameworkCore;
using TaskCRUD.Data;
using TaskCRUD.DTOs.Employee;
using TaskCRUD.Models;

namespace TaskCRUD.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetEmployeeDTO>> GetAllAsync(int? departmentId = null)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .AsQueryable();

            if (departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId.Value);

            return await query
                .Select(e => new GetEmployeeDTO
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
                    DepartmentName = e.Department != null ? e.Department.Name : null
                })
                .ToListAsync();
        }

        public async Task<GetEmployeeDTO?> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return new GetEmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                JobLevel = employee.JobLevel,
                Status = employee.Status,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name
            };
        }

        public async Task<(GetEmployeeDTO? Result, bool DepartmentNotFound)> CreateAsync(CreateEmployeeDTO dto)
        {
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);
            if (!departmentExists) return (null, true);

            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Salary = dto.Salary,
                HireDate = dto.HireDate,
                JobLevel = dto.JobLevel,
                Status = dto.Status,
                DepartmentId = dto.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var department = await _context.Departments.FindAsync(dto.DepartmentId);

            return (new GetEmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                JobLevel = employee.JobLevel,
                Status = employee.Status,
                DepartmentId = employee.DepartmentId,
                DepartmentName = department?.Name
            }, false);
        }

        public async Task<(bool Success, bool NotFound, bool DepartmentNotFound)> UpdateAsync(int id, UpdateEmployeeDTO dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return (false, true, false);

            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);
            if (!departmentExists) return (false, false, true);

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Age = dto.Age;
            employee.Salary = dto.Salary;
            employee.HireDate = dto.HireDate;
            employee.JobLevel = dto.JobLevel;
            employee.Status = dto.Status;
            employee.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();
            return (true, false, false);
        }

        public async Task<(bool Success, bool NotFound, bool DepartmentNotFound)> PatchAsync(int id, PatchEmployeeDTO dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return (false, true, false);

            if (dto.DepartmentId.HasValue)
            {
                var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId.Value);
                if (!departmentExists) return (false, false, true);
                employee.DepartmentId = dto.DepartmentId.Value;
            }

            if (dto.Name != null) employee.Name = dto.Name;
            if (dto.Email != null) employee.Email = dto.Email;
            if (dto.Age.HasValue) employee.Age = dto.Age.Value;
            if (dto.Salary.HasValue) employee.Salary = dto.Salary.Value;
            if (dto.HireDate.HasValue) employee.HireDate = dto.HireDate.Value;
            if (dto.JobLevel.HasValue) employee.JobLevel = dto.JobLevel.Value;
            if (dto.Status.HasValue) employee.Status = dto.Status.Value;

            await _context.SaveChangesAsync();
            return (true, false, false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
