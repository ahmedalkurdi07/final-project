using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class PatientClincForCreate
    {
        public required int PatientId { get; set; }
        public required int ClinicId { get; set; }
        public bool IsActive { get; set; }
    }
}
