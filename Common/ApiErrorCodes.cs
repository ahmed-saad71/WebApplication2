namespace TaskCRUD.Common
{
    // NOTE: Merge these constants into your existing ApiErrorCodes class
    // (the one that already contains CompanyNotFound / CompanyInvalidField).
    public static class ApiErrorCodes
    {
        public const string CompanyNotFound = "COMPANY_NOT_FOUND";
        public const string CompanyInvalidField = "COMPANY_INVALID_FIELD";

        public const string DepartmentNotFound = "DEPARTMENT_NOT_FOUND";
        public const string DepartmentInvalidField = "DEPARTMENT_INVALID_FIELD";

        public const string EmployeeNotFound = "EMPLOYEE_NOT_FOUND";
        public const string EmployeeInvalidField = "EMPLOYEE_INVALID_FIELD";
    }
}
