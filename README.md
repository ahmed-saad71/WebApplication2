# Employee & Department Module

Drop these folders into your existing ASP.NET Core Web API project (the one with the Company/Trainee module).

## Setup steps

1. **Namespace**: All files use `TaskCRUD` as the root namespace. Find-and-replace `TaskCRUD` with your actual project namespace across all files.

2. **Copy folders**: Merge `Models`, `DTOs`, `Services`, `Controllers` into your existing project folders of the same name.

3. **ApiError / ApiErrorCodes**: The `Common/ApiError.cs` file is included only for reference — you already have this class. Do NOT copy it in if it already exists. Instead, open your existing `Common/ApiErrorCodes.cs` and add these four constants to it:
   ```csharp
   public const string DepartmentNotFound = "DEPARTMENT_NOT_FOUND";
   public const string DepartmentInvalidField = "DEPARTMENT_INVALID_FIELD";
   public const string EmployeeNotFound = "EMPLOYEE_NOT_FOUND";
   public const string EmployeeInvalidField = "EMPLOYEE_INVALID_FIELD";
   ```

4. **DbContext**: Add to your `ApplicationDbContext.cs`:
   ```csharp
   public DbSet<Department> Departments { get; set; }
   public DbSet<Employee> Employees { get; set; }
   ```
   And in `OnModelCreating`:
   ```csharp
   modelBuilder.Entity<Employee>()
       .HasOne(e => e.Department)
       .WithMany(d => d.Employees)
       .HasForeignKey(e => e.DepartmentId)
       .OnDelete(DeleteBehavior.Restrict);

   modelBuilder.Entity<Employee>()
       .Property(e => e.Salary)
       .HasColumnType("decimal(18,2)");
   ```

5. **Migration**:
   ```bash
   dotnet ef migrations add AddDepartmentAndEmployee
   dotnet ef database update
   ```

6. **Register services** in `Program.cs`:
   ```csharp
   builder.Services.AddScoped<IDepartmentService, DepartmentService>();
   builder.Services.AddScoped<IEmployeeService, EmployeeService>();
   ```

7. **Build and run**, then check Swagger to confirm all Department/Employee endpoints appear.

## Notes

- `BaseModel` is assumed to already exist in your project with `Id` (int) and `Name` (string) properties, per the Company/Trainee pattern.
- `GET /api/Employees?departmentId={id}` (the stretch goal) is already implemented.
- Adjust `using` statements if your actual folder/namespace layout differs from `Data`, `Models`, `DTOs`, `Services`, `Common`.
