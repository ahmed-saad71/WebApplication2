using TaskCRUD.Models.Enums;

namespace TaskCRUD.DTOs.Employee
{
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public JobLevel JobLevel { get; set; }
        public EmploymentStatus Status { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
