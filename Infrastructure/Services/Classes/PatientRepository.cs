using Application.Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Classes
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }
        public Patient? CreatePatient(int CenterId, PatientForCreate item)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == CenterId && c.IsActive) ;
            if(center == null) return null;
            var newPatient = new Patient()
            {
                AddressId = item.AddressId,
                DOB = item.DOB,
                Email = item.Email,
                FirstName = item.FirstName, 
                Gender  = item.Gender,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber,
                IsActive = item.IsActive,
                
            };
            var patientCenter = new PatientCenter() { CenterId = CenterId, PatientId = newPatient.PatientId, IsActive = true };
            _context.Add(patientCenter);
            newPatient.Centers.Add(patientCenter);

            _context.Patients.Add(newPatient);
            SaveChanges();
            return newPatient;
        }

        public Patient? DeleteById(int ClinicId, int id)
        {
            var patient = GetByIdAsync(ClinicId, id).Result;
            if(patient == null) return null;
            patient.IsActive = false;
            _context.Update(patient);
            SaveChanges();
            return patient;
        }

        public async Task<(List<Patient>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Patients.
                Where(i => i.IsActive &&
                i.Centers!.Any(c => c.CenterId == CenterId && c.IsActive)).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Patients.
                                      Where(c => c.IsActive && 
                                     c.Centers!.Any(c => c.CenterId == CenterId && c.IsActive))
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }
    
        public async Task<Patient?> GetByIdAsync(int CenterId, int id)
        {
            var center = await _context.Centers.FirstOrDefaultAsync(c => c.Id == CenterId && c.IsActive);
            if (center == null) return null;
            var patient = await _context.Patients
                .FirstOrDefaultAsync(
                p => p.PatientId == id &&
                p.IsActive && 
                p.Centers!.Any(c => c.Id == CenterId && c.IsActive));
            if (patient == null) return null;
            return patient;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Patient?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<PatientForUpdate> PatchDocument)
        {
            var patient = GetByIdAsync(CenterId, id).Result;
            if (patient == null) return (null, new List<string> { "patient not found." });
            var patientToPatch = new PatientForUpdate()
            {
                LastName =patient.LastName,
                Gender = patient.Gender,
                FirstName = patient.FirstName,
                Email = patient.Email,
                DOB = patient.DOB,
                Clincs = patient.Clincs,
                Appointments = patient.Appointments,
                AddressId = patient.AddressId,
                Address = patient.Address,
                Invoices = patient.Invoices,
                PhoneNumber = patient.PhoneNumber,
                Treatments = patient.Treatments
            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(patientToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            patient.FirstName = patientToPatch.FirstName;
            patient.LastName = patientToPatch.LastName;
            patient.DOB = patientToPatch.DOB.Value;
            patient.Gender = patientToPatch.Gender.Value;
            patient.PhoneNumber = patientToPatch.PhoneNumber;
            patient.Email = patientToPatch.Email;
            patient.IsActive = patientToPatch.IsActive!.Value;
            patient.Address = patientToPatch.Address;
            patient.AddressId = patientToPatch.AddressId.Value;
            patient.Clincs = patientToPatch.Clincs;
            patient.Centers = patientToPatch.Centers;
            patient.Appointments = patientToPatch.Appointments;
            patient.Treatments = patientToPatch.Treatments;
            patient.Invoices = patientToPatch.Invoices;


            _context.Update(patient);
            await SaveChangesAsync();
            return (patient, errors);

        }

    }
}
