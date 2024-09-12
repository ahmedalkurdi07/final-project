using Application.Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Classes
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public Appointment? CreateAppointment(int centerId, int clinicId, int patientId, AppointmentForCreate item)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == centerId && c.IsActive);
            if (center == null) return null;

            var clinic = _context.Clinics.FirstOrDefault(c => c.CenterId == centerId&& c.Id == clinicId && c.IsActive);
            if (center == null) return null;

            var patient = _context.Patients.FirstOrDefault(c => c.PatientId == patientId  && c.IsActive);
            if (center == null) return null;

            var doctor = clinic!.Doctor;

            var newAppointment = new Appointment()
            {
                DoctorId = item.DoctorId,
                PatientId = item.PatientId,
                AppointmentDate = item.AppointmentDate,
                CenterId = item.CenterId,
                ClinicId = item.ClinicId,
                IsActive = item.IsActive,
                Status = item.Status,
                Center = center,
                Patient = patient,
                Clinic = clinic,
                Doctor = doctor,
            };
            _context.Add(newAppointment);
            SaveChanges();
            return newAppointment;
        }

        public async Task<Appointment?> DeleteById(int centerId, int clinicId, int patientId, int id)
        {
            var appointment = GetByIdAsync(centerId , clinicId, patientId, id).Result;
            if (appointment == null) return null;
            appointment.IsActive = false;
            _context.Update(appointment);
            await SaveChangesAsync();
            return appointment;
        }

        public async Task<(List<Appointment>, PaginationMetaData)> GetAllAsync(int centerId, int clinicId, int pageNumber = 1, int pageSize = 10)
        {

            var totalItemCount = await _context.Appointments.Where(
                a => a.CenterId ==  centerId && a.ClinicId == clinicId && a.IsActive).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Appointments.
                                      Where(a => a.CenterId == centerId && a.ClinicId == clinicId && a.IsActive)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Appointment?> GetByIdAsync(int centerId, int clinicId, int patientId, int id)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == centerId && c.IsActive);
            if (center == null) return null;

            var clinic = _context.Clinics.FirstOrDefault(c => c.CenterId == centerId && c.Id == clinicId && c.IsActive);
            if (center == null) return null;

            var patient = _context.Patients.FirstOrDefault(c => c.PatientId == patientId && c.IsActive);
            if (center == null) return null;

            var doctor = clinic!.Doctor;
            if (doctor == null) return null;

            var appointment = await _context.Appointments.
                FirstOrDefaultAsync(
                a => a.IsActive && a.AppointmentId == id &&
                a.ClinicId == clinicId && a.CenterId == centerId && a.PatientId == patientId );
            if (appointment == null) return null;
            return appointment;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Appointment?, List<string>)> UpdateByIdAsync(int centerId, int clinicId, int patientId, int id, JsonPatchDocument<AppointmentForUpdate> PatchDocument)
        {
            var appointment = GetByIdAsync(centerId , clinicId , patientId, id).Result;
            if(appointment  == null)  return (null, new List<string> { "appointment not found." });
            var appointmentToPatch = new AppointmentForUpdate()
            {
                AppointmentDate = appointment.AppointmentDate,
                Center = appointment.Center,
                CenterId = appointment.CenterId,
                Clinic = appointment.Clinic,
                ClinicId = appointment.ClinicId,
                Doctor = appointment.Doctor,    
                DoctorId = appointment.DoctorId,
                IsActive = appointment.IsActive,
                Patient = appointment.Patient,
                PatientId = appointment.PatientId,
                Status = appointment.Status
            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(appointmentToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            appointment.AppointmentDate = appointmentToPatch.AppointmentDate.Value;
            appointment.Status = appointmentToPatch.Status.Value;
            appointment.IsActive = appointmentToPatch.IsActive.Value;
            appointment.PatientId = appointmentToPatch.PatientId.Value;
            appointment.Patient = appointmentToPatch.Patient;
            appointment.Doctor = appointmentToPatch.Doctor;
            appointment.DoctorId = appointmentToPatch.DoctorId.Value;
            appointment.Center = appointmentToPatch.Center;
            appointment.CenterId = appointmentToPatch.CenterId.Value;
            appointment.Clinic = appointmentToPatch.Clinic;
            appointment.ClinicId = appointmentToPatch.ClinicId.Value;

            _context.Update(appointment);
            await SaveChangesAsync();
            return(appointment , errors);

        }
    }
}
