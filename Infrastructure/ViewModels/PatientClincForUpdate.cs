using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class PatientClincForUpdate
    {
        public  int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public bool IsActive { get; set; }
    }
}
