using Application.Domain.Models;
using Infrastructure.Services.Classes;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly ICenterRepository _centerRepository;
        private readonly int maxPageSize = 10;

        public CentersController(ICenterRepository centerRepository)
        {
            _centerRepository = centerRepository;
        }
        [HttpGet("{centerId}", Name = "GetCenter")]
        public async Task<ActionResult<Center>> GetCenter(int centerId)
        {
            var center = await _centerRepository.GetByIdAsync(centerId);
            if (center == null) return NotFound();
            return Ok(center);
        }
        [HttpGet(Name = "GetAllCenters")]
        public async Task<ActionResult<Center>> GetAllCenters(int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (centers, paginationMetaData) = await _centerRepository.GetAllAsync(pageNumber , pageSize);
            if (centers == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(centers);
        }

        [HttpPatch("{centerId}")]
        public async Task<ActionResult> UpdateCenter(int centerId, JsonPatchDocument<CenterForUpdate> patchDocument)
        {
            var (updatedCenter, errors) = await _centerRepository.UpdateByIdAsync(centerId, patchDocument);

            // تحقق من وجود الأخطاء
            if (errors.Count > 0)
            {
                // إرجاع الأخطاء إذا كانت موجودة
                return BadRequest(errors);
            }

            // التحقق من وجود العنوان
            if (updatedCenter == null)
            {
                return NotFound();
            }

            // إذا تم التحديث بنجاح وبدون أخطاء
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Patient> CreateCenter(CenterForCreate center)
        {
            try
            {
                var newCenter = _centerRepository.CreateCenter(center);
                if (newCenter == null)
                    return BadRequest();
                return CreatedAtRoute("GetCenter", new { centerId = newCenter.Id }, newCenter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{centerId}")]
        public ActionResult<Address> DeleteCenter(int centerId)
        {
            var center = _centerRepository.DeleteById(centerId);
            if (center == null) return NotFound();

            return NoContent();
        }
    }
}
