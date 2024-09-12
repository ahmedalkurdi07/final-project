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
    [Route("Centers/{CenterId}/Doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly int maxPageSize = 10;
        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        [HttpGet("{doctorId}", Name = "GetDoctor")]
        public async Task<ActionResult<Doctor>> GetDoctor( int CenterId, int doctorId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(CenterId, doctorId);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }
        [HttpGet(Name = "GetAllDoctors")]
        public async Task<ActionResult<Doctor>> GetAllDoctors(int CenterId,int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (doctors, paginationMetaData) = await _doctorRepository.GetAllAsync(CenterId,pageNumber ,pageSize);
            if (doctors == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(doctors);
        }
        [HttpPatch("{doctorId}")]
        public async Task<ActionResult> UpdateDoctor(int CenterId,int doctorId, JsonPatchDocument<DoctorForUpdate> patchDocument)
        {
            var (updatedDoctor, errors) = await _doctorRepository.UpdateByIdAsync(CenterId,doctorId, patchDocument);

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            if (updatedDoctor == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost]
        public ActionResult<Doctor> CreateDoctor( int CenterId,DoctorForCreate doctor)
        {
            try
            {
                var newDoctor = _doctorRepository.CreateDoctor(CenterId, doctor);
                if (newDoctor == null)
                    return BadRequest();
                return CreatedAtRoute("GetDoctor", new { CenterId = newDoctor.CenterId , doctorId = newDoctor.DoctorId }, newDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{doctorId}")]
        public ActionResult<Address> DeleteAddress(int CenterId , int doctorId)
        {
            var doctor = _doctorRepository.DeleteById( CenterId, doctorId);
            if (doctor == null) return NotFound();

            return NoContent();
        }
    }
}
