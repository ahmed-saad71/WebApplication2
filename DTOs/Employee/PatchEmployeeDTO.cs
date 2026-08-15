using TaskCRUD.Models.Enums;

namespace TaskCRUD.DTOs.Employee
{
    public class PatchEmployeeDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public JobLevel? JobLevel { get; set; }
        public EmploymentStatus? Status { get; set; }
        public int? DepartmentId { get; set; }
    }
}
