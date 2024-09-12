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
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int ClinicId, int id);
        Task<(List<Employee>, PaginationMetaData)> GetAllAsync(int ClinicId, int pageNumber, int pageSize);
        Employee? DeleteById(int ClinicId, int id);
        Task<(Employee?, List<string>)> UpdateByIdAsync(int ClinicId, int id, JsonPatchDocument<EmployeeForUpdate> PatchDocument);

        Employee? CreateEmployee(int ClinicId, EmployeeForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }

}
