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
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context = context;
        }
        public Clinic? CreateClinic(int CenterId, ClinicForCreate item)
        {
            if (item == null) return null;
            var center = _context.Centers.FirstOrDefault(c => c.IsActive && c.Id == CenterId);
            if (center == null) return null;
            var newClinic = new Clinic()
            {
                Name = item.Name,
                AddressId = item.AddressId,
                CenterId = item.CenterId,
                Specialty = item.Specialty,
                IsActive = item.IsActive,

            };
            _context.Add(newClinic);
            SaveChanges();
            return newClinic;
        }

        public Clinic? DeleteById(int CenterId, int id)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == CenterId);
            if (center == null) return null;

            var clinic = _context.Clinics.
                FirstOrDefault(c => c.Id == id && c.IsActive && c.CenterId == CenterId);
            if (clinic == null) return null;

            clinic.IsActive = false;
            _context.Update(clinic);
            SaveChanges();
            return clinic;
        }

        public async Task<(List<Clinic>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Clinics.Where(i => i.IsActive && i.CenterId == CenterId).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Clinics.Where(c => c.IsActive && c.CenterId == CenterId)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Clinic?> GetByIdAsync(int CenterId, int id)
        {
            var center = await _context.Centers.FirstOrDefaultAsync(c => c.IsActive && c.Id == CenterId);
            if (center == null) return null;

            var clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.IsActive && c.CenterId == CenterId);
            if (clinic == null) return null;

            return clinic;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Clinic?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<ClinicForUpdate> PatchDocument)
        {
            var center = await _context.Centers.FirstOrDefaultAsync(c => c.IsActive && c.Id == CenterId);
            if (center == null) return (null, new List<string> { "Center not found" });

            var clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.IsActive && c.CenterId == CenterId);
            if (clinic == null) return (null, new List<string> { "Clinic not found" });

            var clinicToPatch = new ClinicForUpdate()
            {

                Address = clinic.Address,
                AddressId = clinic.AddressId,
                Center = clinic.Center,
                CenterId = clinic.CenterId,
                Employees = clinic.Employees,
                IsActive = clinic.IsActive,
                Name = clinic.Name,
                Patients = clinic.Patients,
                Specialty = clinic.Specialty,

            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(clinicToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            clinic.Address = clinicToPatch.Address;
            clinic.AddressId = clinicToPatch.AddressId.Value;
            clinic.Center = clinicToPatch.Center;
            clinic.CenterId = clinicToPatch.CenterId.Value;
            clinic.Employees = clinicToPatch.Employees;
            clinic.IsActive = clinicToPatch.IsActive.Value;
            clinic.Name = clinicToPatch.Name;
            clinic.Patients = clinicToPatch.Patients;
            clinic.Specialty = clinicToPatch.Specialty.Value;

            _context.Update(clinic);
            await SaveChangesAsync();
            return (clinic, errors);

        }
    }
}
