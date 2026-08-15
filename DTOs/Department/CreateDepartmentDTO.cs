using System.ComponentModel.DataAnnotations;

namespace TaskCRUD.DTOs.Department
{
    public class CreateDepartmentDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;
    }
}
