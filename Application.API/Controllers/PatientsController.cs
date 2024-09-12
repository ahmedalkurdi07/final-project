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
    [Route("Centers/{CenterId}/Patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly int maxPageSize = 10;

        public PatientsController(IPatientRepository patientRepository) 
        {
            _patientRepository = patientRepository;
        }
        [HttpGet("{patientId}", Name = "GetPatient")]
        public async Task<ActionResult<Patient>> GetPatient(int CenterId, int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(CenterId, patientId);
            if (patient == null) return NotFound();
            return Ok(patient);
        }
        [HttpGet(Name = "GetAllPatients")]
        public async Task<ActionResult<Patient>> GetAllPatients(int CenterId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (patients, paginationMetaData) = await _patientRepository.GetAllAsync(CenterId, pageNumber, pageSize);
            if (patients == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(patients);
        }
        [HttpPatch("{patientId}")]
        public async Task<ActionResult> UpdatePatient(int CenterId, int patientId, JsonPatchDocument<PatientForUpdate> patchDocument)
        {
            var (updatedPatient, errors) = await _patientRepository.UpdateByIdAsync(CenterId, patientId, patchDocument);

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            if (updatedPatient == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost]
        public ActionResult<Patient> CreatePatient(int CenterId, PatientForCreate patient)
        {
            try
            {
                var newPatient = _patientRepository.CreatePatient(CenterId, patient);
                if (newPatient == null)
                    return BadRequest();
                return CreatedAtRoute("GetPatient", new { CenterId = CenterId, patientId = newPatient.PatientId }, newPatient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{patientId}")]
        public ActionResult<Address> DeleteAddress(int CenterId, int patientId)
        {
            var patient = _patientRepository.DeleteById(CenterId, patientId);
            if (patient == null) return NotFound();

            return NoContent();
        }
    }
}
