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
    public interface IClinicRepository
    {
        Task<Clinic?> GetByIdAsync(int CenterId, int id);
        Task<(List<Clinic>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize);
        Clinic? DeleteById(int CenterId, int id);
        Task<(Clinic?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<ClinicForUpdate> PatchDocument);

        Clinic? CreateClinic(int CenterId, ClinicForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }

}
