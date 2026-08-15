using TaskCRUD.DTOs.Employee;

namespace TaskCRUD.DTOs.Department
{
    public class GetDepartmentWithEmployeesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<GetEmployeeDTO> Employees { get; set; } = new();
    }
}
