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
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(int ClinicId, int id);
        Task<(List<Invoice>, PaginationMetaData)> GetAllAsync(int ClinicId, int pageNumber, int pageSize);
        Invoice? DeleteById(int ClinicId, int id);
        Task<(Invoice?, List<string>)> UpdateByIdAsync(int ClinicId, int id, JsonPatchDocument<InvoiceForUpdate> PatchDocument);

        Invoice? CreateInvoice(int ClinicId, InvoiceForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }

}
