using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class TreatmentForCreate
    {
        public required DateTime TreatmentDate { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }


        public int DoctorId { get; set; }

        public int ClinicId { get; set; }

        public int PatientId { get; set; }

        public int InvoiceId { get; set; }

    }
}
