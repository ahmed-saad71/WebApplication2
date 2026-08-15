using System.ComponentModel.DataAnnotations;
using TaskCRUD.Models.Enums;

namespace TaskCRUD.DTOs.Employee
{
    public class CreateEmployeeDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Range(16, 100)]
        public int Age { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public JobLevel JobLevel { get; set; }

        [Required]
        public EmploymentStatus Status { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
