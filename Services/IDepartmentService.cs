using TaskCRUD.DTOs.Department;

namespace TaskCRUD.Services
{
    public interface IDepartmentService
    {
        Task<List<GetDepartmentDTO>> GetAllAsync();
        Task<GetDepartmentDTO?> GetByIdAsync(int id);
        Task<GetDepartmentWithEmployeesDTO?> GetWithEmployeesAsync(int id);
        Task<GetDepartmentDTO> CreateAsync(CreateDepartmentDTO dto);
        Task<bool> UpdateAsync(int id, UpdateDepartmentDTO dto);
        Task<bool> PatchAsync(int id, PatchDepartmentDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
