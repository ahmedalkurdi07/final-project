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
    public interface ITreatmentRepository
    {
        Task<Treatment?> GetByIdAsync(int ClinicId, int id);
        Task<(List<Treatment>, PaginationMetaData)> GetAllAsync(int ClinicId, int pageNumber, int pageSize);
        Treatment? DeleteById(int ClinicId, int id);
        Task<(Treatment?, List<string>)> UpdateByIdAsync(int ClinicId, int id, JsonPatchDocument<TreatmentForUpdate> PatchDocument);

        Treatment? CreateTreatment(int ClinicId, TreatmentForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
