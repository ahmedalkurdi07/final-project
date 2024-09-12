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
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }
        public Address? CreateAddress(AddressForCreate item)
        {
            if(item == null) return null;
            var newAddress = new Address()
            {
                City = item.City,
                Country = item.Country,
                PostalCode = item.PostalCode,
                StreetNumber = item.StreetNumber,
                IsActive = item.IsActive,
            };
            _context.Add(newAddress);
            SaveChanges();
            return newAddress;
        }

        public Address? DeleteById(int id)
        {
            var address =  _context.Addresses.FirstOrDefault(a => a.IsActive && a.AddressId == id);
            if (address == null) return null;
            address.IsActive = false;
            _context.Update(address);
            SaveChanges();
            return address;
        }

        public async Task<(List<Address>, PaginationMetaData)> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var totalItemCount = await _context.Addresses.Where(i => i.IsActive).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Addresses
                                    .Where(i => i.IsActive)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.IsActive && a.AddressId == id);
            if(address == null) return null;
            return address;
        }

        public async Task<(Address?, List<string>)> UpdateByIdAsync(int id, JsonPatchDocument<AddressForUpdate> PatchDocument)
        {
            var address = await GetByIdAsync(id);
            if (address == null) return (null, new List<string> { "Address not found." });

            var AddressToPatch = new AddressForUpdate()
            {
                IsActive = address.IsActive,
                City = address.City,
                Country = address.Country,
                Doctors = address.Doctors,
                Employees = address.Employees,
                Patients = address.Patients,
                PostalCode = address.PostalCode,
                StreetNumber = address.StreetNumber,
                Centers = address.Centers,
                Clinics = address.Clinics,
                
            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(AddressToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (null, errors);
            }

            address.Country = AddressToPatch.Country;
            address.City = AddressToPatch.City;
            address.StreetNumber = AddressToPatch.StreetNumber;
            address.PostalCode = AddressToPatch.PostalCode;
            address.IsActive = (bool)AddressToPatch.IsActive;
            address.Patients = AddressToPatch.Patients;
            address.Clinics = AddressToPatch.Clinics;
            address.Centers = AddressToPatch.Centers;
            address.Doctors = AddressToPatch.Doctors;
            address.Employees = AddressToPatch.Employees;

            _context.Update(address);
            await SaveChangesAsync(); 
            return (address, errors);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
