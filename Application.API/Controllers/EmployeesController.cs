using Application.Domain.Models;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.API.Controllers
{
    [Route("Centers/{CenterId}/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly int maxPageSize = 10;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet("{employeeId}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee(int CenterId, int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(CenterId, employeeId);
            if (employee == null) return NotFound();
            return Ok(employee);
        }
        [HttpGet(Name = "GetAllEmployees")]
        public async Task<ActionResult<Employee>> GetAllEmployees(int CenterId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (employees, paginationMetaData) = await _employeeRepository.GetAllAsync(CenterId, pageNumber, pageSize);
            if (employees == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(employees);
        }
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult> UpdateEmployee(int CenterId, int employeeId, JsonPatchDocument<EmployeeForUpdate> patchDocument)
        {
            var (updatedEmployee, errors) = await _employeeRepository.UpdateByIdAsync(CenterId, employeeId, patchDocument);

            // تحقق من وجود الأخطاء
            if (errors.Count > 0)
            {
                // إرجاع الأخطاء إذا كانت موجودة
                return BadRequest(errors);
            }

            // التحقق من وجود العنوان
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            // إذا تم التحديث بنجاح وبدون أخطاء
            return NoContent();
        }
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(int CenterId, EmployeeForCreate employee)
        {
            try
            {
                var newEmployee = _employeeRepository.CreateEmployee(CenterId, employee);
                if (newEmployee == null)
                    return BadRequest();
                return CreatedAtRoute("GetEmployee", new { CenterId = newEmployee.CenterId, employeeId = newEmployee.EmployeeId }, newEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{employeeId}")]
        public ActionResult<Address> DeleteAddress(int CenterId, int employeeId)
        {
            var employee = _employeeRepository.DeleteById(CenterId, employeeId);
            if (employee == null) return NotFound();

            return NoContent();
        }
    }
}