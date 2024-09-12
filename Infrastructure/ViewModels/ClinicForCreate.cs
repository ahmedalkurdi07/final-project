using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class ClinicForCreate
    {
        public required string Name { get; set; }
        public required Specialty Specialty { get; set; }
        public bool IsActive { get; set; }


        public required int CenterId { get; set; }
        public required int AddressId { get; set; }

    }
}
