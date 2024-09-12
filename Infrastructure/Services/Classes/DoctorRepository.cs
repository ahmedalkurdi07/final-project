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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }
        public Doctor? CreateDoctor(int CenterId, DoctorForCreate item)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == CenterId && c.IsActive);
            if (center == null) return null;

            var newDoctor = new Doctor()
            {
                Specialty = item.Specialty,
                ConsultationHours = item.ConsultationHours,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Gender = item.Gender,
                ClinicId = item.ClinicId,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                AddressId = item.AddressId,
                IsActive = item.IsActive,
                CenterId = item.CenterId,
                
            };

            _context.Doctors.Add(newDoctor);
            SaveChanges();

            return newDoctor;
        }

        public Doctor? DeleteById(int CenterId, int id)
        {
            var doctor = GetByIdAsync(CenterId , id).Result;

            if (doctor == null)
                return null;

            doctor.IsActive = false;

            _context.Doctors.Update(doctor);
            SaveChanges();

            return doctor;
        }

        public async Task<(List<Doctor>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Doctors.Where(i => i.IsActive && i.CenterId == CenterId).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Doctors.Where(c => c.IsActive)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Doctor?> GetByIdAsync(int CenterId, int id)
        {
            var center = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == CenterId && c.IsActive) ;
            if (center == null) return null;

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(p => p.DoctorId == id && p.IsActive && p.ClinicId == CenterId);

            if (doctor == null)
                return null;

            return doctor;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Doctor?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<DoctorForUpdate> PatchDocument)
        {
            var doctor = GetByIdAsync(CenterId, id).Result;
            if (doctor == null) return (null, new List<string> { "doctor not found." }); 

            var doctorToPatch = new DoctorForUpdate()
            {
                AddressId = doctor.DoctorId,
                Address = doctor.Address,
                Appointments = doctor.Appointments,
                ConsultationHours = doctor.ConsultationHours,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                Gender = doctor.Gender,
                IsActive = doctor.IsActive,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                ClinicId = doctor.ClinicId,
                Clinic = doctor.Clinic,
                CenterId = doctor.CenterId,
                Center = doctor.Center,

            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(doctorToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            doctor.FirstName = doctorToPatch.FirstName;
            doctor.LastName = doctorToPatch.LastName;
            doctor.Specialty = doctorToPatch.Specialty.Value;
            doctor.PhoneNumber = doctorToPatch.PhoneNumber;
            doctor.Email = doctorToPatch.Email;
            doctor.Gender = doctorToPatch.Gender.Value;
            doctor.IsActive = doctorToPatch.IsActive.Value;
            doctor.ConsultationHours = doctorToPatch.ConsultationHours.Value;

            doctor.AddressId = doctorToPatch.AddressId.Value;
            doctor.Address = doctorToPatch.Address;

            doctor.Clinic = doctorToPatch.Clinic;
            doctor.ClinicId = doctorToPatch.ClinicId.Value;

            doctor.Center = doctorToPatch.Center;
            doctor.CenterId = doctorToPatch.CenterId.Value;

            doctor.Appointments = doctorToPatch.Appointments;

            _context.Update(doctor);
            await SaveChangesAsync();
            return (doctor, errors);
        }
    }
}
