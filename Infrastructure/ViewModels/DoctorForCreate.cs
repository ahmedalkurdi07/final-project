using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class DoctorForCreate
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Specialty Specialty { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public ConsultationHours ConsultationHours { get; set; }


        public required int AddressId { get; set; }

        public int ClinicId { get; set; }

        public int CenterId { get; set; }

    }
}
