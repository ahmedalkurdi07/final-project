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
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int CenterId, int id);
        Task<(List<Doctor>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize);
        Doctor? DeleteById(int CenterId, int id);
        Task<(Doctor?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<DoctorForUpdate> PatchDocument);

        Doctor? CreateDoctor(int CenterId, DoctorForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }

}
