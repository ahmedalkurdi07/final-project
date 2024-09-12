using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class PatientForCreate
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DOB { get; set; }
        public required Gender Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required int AddressId { get; set; }
        public bool IsActive { get; set; } = true;
        public List<PatientCenter>? Centers = new List<PatientCenter>();

    }
}
