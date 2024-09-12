using Application.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DOB { get; set; }
        public required Gender Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }

        public required int AddressId { get; set; }
        public Address? Address { get; set; }
        
        public List<PatientClinc>? Clincs = new List<PatientClinc>();

        public List<PatientCenter>? Centers = new List<PatientCenter>();

        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();
        
        public List<Treatment>? Treatments { get; set; } = new List<Treatment>();
        
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();
        


    }
}
