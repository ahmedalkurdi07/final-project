using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class CenterForUpdate
    {
        public  string? Name { get; set; }
        public  string? PhoneNumber { get; set; }
        public  string? Email { get; set; }
        public bool? IsActive { get; set; }

        public  int? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }
}
