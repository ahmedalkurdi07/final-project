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
    public interface ICenterRepository
    {
        Task<Center?> GetByIdAsync(int id);
        Task<(List<Center>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize);
        Center? DeleteById(int id);
        Task<(Center?, List<string>)> UpdateByIdAsync(int id, JsonPatchDocument<CenterForUpdate> PatchDocument);

        Center CreateCenter(CenterForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
