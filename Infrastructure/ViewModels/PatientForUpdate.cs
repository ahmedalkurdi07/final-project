using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class PatientForUpdate
    {
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public  DateTime? DOB { get; set; }
        public  Gender? Gender { get; set; }
        public  string? PhoneNumber { get; set; }
        public  string? Email { get; set; }
        public bool? IsActive { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public List<PatientClinc>? Clincs = new List<PatientClinc>();

        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();

        public List<Treatment>? Treatments { get; set; } = new List<Treatment>();

        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();
        public List<PatientCenter>? Centers = new List<PatientCenter>();
    }
}
