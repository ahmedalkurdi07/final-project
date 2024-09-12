using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class AddressForCreate
    {
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string StreetNumber { get; set; }
        public required string PostalCode { get; set; }
        public  bool IsActive { get; set; } = true;

/*
        public List<Patient>? Patients { get; set; } = new List<Patient>();
        public List<Clinic>? Clinics { get; set; } = new List<Clinic>();
        public List<Center>? Centers { get; set; } = new List<Center>();
        public List<Doctor>? Doctors { get; set; } = new List<Doctor>();
        public List<Employee>? Employees { get; set; } = new List<Employee>();
*/
    }
}
