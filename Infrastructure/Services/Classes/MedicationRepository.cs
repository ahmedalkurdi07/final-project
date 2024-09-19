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
    public class MedicationRepository : IMedicationRepository
    {
        private readonly AppDbContext _context;

        public MedicationRepository(AppDbContext context)
        {
            _context = context;
        }
        public Medication? CreateMedication(MedicationForCreate item)
        {
            if(item == null) return null;
            var newMedication = new Medication()
            {
                Description = item.Description,
                Name = item.Name,
                Price = item.Price,
            };
            _context.Add(newMedication);
            SaveChanges();
            return newMedication;
        }
        /*
        public Medication? DeleteById(int id)
        {
            var medication = _context.Medications.FirstOrDefault(m => m.MedicationId == id);
            if(medication == null) return null;
            
            return medication;
        }
        */
        public Task<(List<Medication>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Medication?> GetByIdAsync(int id)
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null) return null;

            return medication;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<(Medication?, List<string>)> UpdateByIdAsync(int id, JsonPatchDocument<MedicationForUpdate> PatchDocument)
        {
            throw new NotImplementedException();
        }
    }
}
