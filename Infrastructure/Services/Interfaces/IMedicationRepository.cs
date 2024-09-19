using Application.Domain.Models;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IMedicationRepository
    {
        Task<Medication?> GetByIdAsync( int id);
        Task<(List<Medication>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize);
       // Medication? DeleteById( int id);
        Task<(Medication?, List<string>)> UpdateByIdAsync( int id, JsonPatchDocument<MedicationForUpdate> PatchDocument);

        Medication? CreateMedication( MedicationForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
