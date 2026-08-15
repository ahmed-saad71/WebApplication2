using Microsoft.AspNetCore.Mvc;
using TaskCRUD.Common;
using TaskCRUD.DTOs.Employee;
using TaskCRUD.Services;

namespace TaskCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? departmentId)
        {
            var employees = await _service.GetAllAsync(departmentId);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeNotFound,
                    Message = $"There is no employee with this Id : {id}",
                    Errors = null
                });
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeInvalidField,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var (result, departmentNotFound) = await _service.CreateAsync(dto);
            if (departmentNotFound)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeInvalidField,
                    Message = $"There is no department with this Id : {dto.DepartmentId}",
                    Errors = null
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeInvalidField,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var (success, notFound, departmentNotFound) = await _service.UpdateAsync(id, dto);

            if (notFound)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeNotFound,
                    Message = $"There is no employee with this Id : {id}",
                    Errors = null
                });
            }

            if (departmentNotFound)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeInvalidField,
                    Message = $"There is no department with this Id : {dto.DepartmentId}",
                    Errors = null
                });
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchEmployeeDTO dto)
        {
            var (success, notFound, departmentNotFound) = await _service.PatchAsync(id, dto);

            if (notFound)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeNotFound,
                    Message = $"There is no employee with this Id : {id}",
                    Errors = null
                });
            }

            if (departmentNotFound)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeInvalidField,
                    Message = $"There is no department with this Id : {dto.DepartmentId}",
                    Errors = null
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.EmployeeNotFound,
                    Message = $"There is no employee with this Id : {id}",
                    Errors = null
                });
            }

            return NoContent();
        }
    }
}
