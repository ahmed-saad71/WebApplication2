using System.ComponentModel.DataAnnotations;

namespace TaskCRUD.Models
{
    public class Department : BaseModel
    {
        [Required]
        public string Location { get; set; } = string.Empty;

        public List<Employee> Employees { get; set; } = new();
    }
}
