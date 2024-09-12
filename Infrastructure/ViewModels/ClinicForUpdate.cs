using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class ClinicForUpdate
    {
        public  string? Name { get; set; }
        public Specialty? Specialty { get; set; }
        public bool? IsActive { get; set; }


        public int? CenterId { get; set; }
        public Center? Center { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public List<Employee>? Employees { get; set; } = new List<Employee>();
        public List<PatientClinc> Patients = new List<PatientClinc>();
        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();
    }
}
