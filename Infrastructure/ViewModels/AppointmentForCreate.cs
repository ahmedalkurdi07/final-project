using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class AppointmentForCreate
    {
        public required DateTime AppointmentDate { get; set; }
        public required AppointmentStatus Status { get; set; }
        public  bool IsActive { get; set; }

        public required int PatientId { get; set; }

        public required int DoctorId { get; set; }

        public  int CenterId { get; set; }

        public required int ClinicId { get; set; }

    }
}
