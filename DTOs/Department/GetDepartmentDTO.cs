namespace TaskCRUD.DTOs.Department
{
    public class GetDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
