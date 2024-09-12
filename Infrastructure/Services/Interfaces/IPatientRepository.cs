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
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int CenterId, int id);
        Task<(List<Patient>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize);
        Patient? DeleteById(int ClinicId, int id);
        Task<(Patient?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<PatientForUpdate> PatchDocument);

        Patient? CreatePatient(int CenterId, PatientForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();

    }

}
