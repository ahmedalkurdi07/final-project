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
    [Route("Centers/{CenterId}/Clinics")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly int maxPageSize = 10;

        public ClinicsController(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        [HttpGet("{clinicId}", Name = "GetClinic")]
        public async Task<ActionResult<Clinic>> GetClinic(int CenterId, int clinicId)
        {
            var Clinic = await _clinicRepository.GetByIdAsync(CenterId, clinicId);
            if (Clinic == null) return NotFound();
            return Ok(Clinic);
        }
        [HttpGet(Name = "GetAllClinices")]
        public async Task<ActionResult<Clinic>> GetAllClinices(int CenterId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (Clinices, paginationMetaData) = await _clinicRepository.GetAllAsync(CenterId,  pageNumber, pageSize);
            if (Clinices == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(Clinices);
        }

        [HttpDelete("{ClinicId}")]
        public ActionResult<Clinic> DeleteClinic(int CenterId, int clinicId)
        {
            var Clinic = _clinicRepository.DeleteById(CenterId, clinicId);
            if (Clinic == null) return NotFound();

            return NoContent();
        }

        [HttpPatch("{ClinicId}")]
        public async Task<ActionResult> UpdateClinic(int CenterId, int clinicId, JsonPatchDocument<ClinicForUpdate> patchDocument)
        {
            var (updatedClinic, errors) = await _clinicRepository.UpdateByIdAsync(CenterId, clinicId, patchDocument);

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            if (updatedClinic == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost]
        public ActionResult<Patient> CreateClinic(int CenterId, ClinicForCreate Clinic)
        {
            try
            {
                var newClinic = _clinicRepository.CreateClinic(CenterId, Clinic);
                if (newClinic == null)
                    return BadRequest();
                return CreatedAtRoute("GetClinic", new {CenterId = newClinic.CenterId  ,ClinicId = newClinic.Id }, newClinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex + "Internal server error");
            }
        }

    }
}
