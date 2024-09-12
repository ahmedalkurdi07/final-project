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
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<(List<T>, PaginationMetaData)> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
