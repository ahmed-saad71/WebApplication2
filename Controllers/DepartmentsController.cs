using Microsoft.AspNetCore.Mvc;
using TaskCRUD.Common;
using TaskCRUD.DTOs.Department;
using TaskCRUD.Services;

namespace TaskCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentNotFound,
                    Message = $"There is no department with this Id : {id}",
                    Errors = null
                });
            }
            return Ok(department);
        }

        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetWithEmployees(int id)
        {
            var department = await _service.GetWithEmployeesAsync(id);
            if (department == null)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentNotFound,
                    Message = $"There is no department with this Id : {id}",
                    Errors = null
                });
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentInvalidField,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentInvalidField,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentNotFound,
                    Message = $"There is no department with this Id : {id}",
                    Errors = null
                });
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchDepartmentDTO dto)
        {
            var patched = await _service.PatchAsync(id, dto);
            if (!patched)
            {
                return NotFound(new ApiError
                {
                    Code = ApiErrorCodes.DepartmentNotFound,
                    Message = $"There is no department with this Id : {id}",
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
                    Code = ApiErrorCodes.DepartmentNotFound,
                    Message = $"There is no department with this Id : {id}",
                    Errors = null
                });
            }

            return NoContent();
        }
    }
}
