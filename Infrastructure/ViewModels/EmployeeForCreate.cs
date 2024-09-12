using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class EmployeeForCreate
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public required Gender Gender { get; set; }
        public required int AddressId { get; set; }
        
        public required int ClinicId { get; set; }

        public required int CenterId { get; set; }

    }
}
