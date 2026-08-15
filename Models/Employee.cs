using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskCRUD.Models.Enums;

namespace TaskCRUD.Models
{
    public class Employee : BaseModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public JobLevel JobLevel { get; set; }

        [Required]
        public EmploymentStatus Status { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }
    }
}
