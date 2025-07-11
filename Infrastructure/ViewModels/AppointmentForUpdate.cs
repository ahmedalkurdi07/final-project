﻿using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class AppointmentForUpdate
    {
        public DateTime? AppointmentDate { get; set; }
        public AppointmentStatus? Status { get; set; }
        public bool? IsActive { get; set; }

        public  int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public  int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int? CenterId { get; set; }
        public Center? Center { get; set; }

        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

    }
}
