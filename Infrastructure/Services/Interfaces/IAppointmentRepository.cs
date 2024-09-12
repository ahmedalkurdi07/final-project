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
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int centerId, int clinicId, int patientId, int id);
        Task<(List<Appointment>, PaginationMetaData)> GetAllAsync(int centerId, int clinicId, /*int patientId, */int pageNumber = 1, int pageSize = 10);
        Task<Appointment?> DeleteById(int centerId, int clinicId, int patientId, int id);
        Task<(Appointment?, List<string>)> UpdateByIdAsync(int centerId, int clinicId, int patientId, int id, JsonPatchDocument<AppointmentForUpdate> PatchDocument);

        Appointment? CreateAppointment(int centerId, int clinicId, int patientId, AppointmentForCreate item);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
