using TaskCRUD.DTOs.Employee;

namespace TaskCRUD.Services
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeeDTO>> GetAllAsync(int? departmentId = null);
        Task<GetEmployeeDTO?> GetByIdAsync(int id);
        Task<(GetEmployeeDTO? Result, bool DepartmentNotFound)> CreateAsync(CreateEmployeeDTO dto);
        Task<(bool Success, bool NotFound, bool DepartmentNotFound)> UpdateAsync(int id, UpdateEmployeeDTO dto);
        Task<(bool Success, bool NotFound, bool DepartmentNotFound)> PatchAsync(int id, PatchEmployeeDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
