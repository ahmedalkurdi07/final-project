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
    public class CenterRepository : ICenterRepository
    {
        private readonly AppDbContext _context;

        public CenterRepository(AppDbContext context)
        {
            _context = context;
        }
        public Center CreateCenter(CenterForCreate item)
        {
            var newCenter = new Center()
            {
                AddressId = item.AddressId,
                Email = item.Email,
                Name = item.Name,
                PhoneNumber = item.PhoneNumber,
                IsActive = item.IsActive,
            };
            _context.Centers.Add(newCenter);
            SaveChanges();
            return newCenter;
        }

        public Center? DeleteById(int id)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == id);
            if (center == null) return null;

            center.IsActive = false;

            _context.Update(center);
            SaveChanges();

            return center;
        }

        public async Task<(List<Center>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Centers.Where(i => i.IsActive).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Centers.Where(c => c.IsActive)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Center?> GetByIdAsync(int id)
        {
            var center = await _context.Centers.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            if (center == null) return null;
            return center;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Center?, List<string>)> UpdateByIdAsync(int id, JsonPatchDocument<CenterForUpdate> PatchDocument)
        {
            var center = await _context.Centers.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            if (center == null) return (null, new List<string> { " Center not found or deleted" });
            var centerToPatch = new CenterForUpdate()
            {
                Address = center.Address,
                IsActive = center.IsActive,
                AddressId = center.AddressId,
                Clinics = center.Clinics,
                Email = center.Email,
                Name = center.Name,
                PhoneNumber = center.PhoneNumber,
                Appointments =  center.Appointments,

            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(centerToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            center.Name = centerToPatch.Name;
            center.PhoneNumber = centerToPatch.PhoneNumber;
            center.Email = centerToPatch.Email;
            center.IsActive = centerToPatch.IsActive.Value;
            center.AddressId = centerToPatch.AddressId.Value;
            center.Address = centerToPatch.Address;
            center.Appointments = centerToPatch.Appointments;
            center.Clinics = centerToPatch.Clinics;


            _context.Update(center);
            await SaveChangesAsync();

            return (center, errors);
        }
    }
}
