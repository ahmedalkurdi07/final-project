using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class EmployeeForUpdate
    {
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public  string? Role { get; set; }
        public  string? PhoneNumber { get; set; }
        public  string? Email { get; set; }
        public bool? IsActive { get; set; }
        public  Gender? Gender { get; set; }

        public  int? AddressId { get; set; }
        public Address? Address { get; set; }

        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

        public int? CenterId { get; set; }
        public Center? Center { get; set; }
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();
    }
}
