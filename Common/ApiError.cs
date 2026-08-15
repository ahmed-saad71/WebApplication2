namespace TaskCRUD.Common
{
    // NOTE: This should already exist from your Company/Trainee module.
    // Included here only for reference - do not duplicate it in your project.
    public class ApiError
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
    }
}
