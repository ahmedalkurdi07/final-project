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
    public interface IAddressRepository
    {
        Task<Address?> GetByIdAsync(int id);
        Task<(List<Address>, PaginationMetaData)> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Address? DeleteById(int id);
        Task<(Address?, List<string>)> UpdateByIdAsync(int id, JsonPatchDocument<AddressForUpdate> PatchDocument);
        Address? CreateAddress(AddressForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
