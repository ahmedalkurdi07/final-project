using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class InvoiceForUpdate
    {
        public decimal? Amount { get; set; }
        public PaymentStatus? Status { get; set; }
        public bool? IsActive { get; set; }

        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? TreatmentId { get; set; }
        public Treatment? Treatment { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public Clinic? Clinic { get; set; }
        public int? ClinicId { get; set; }

        public int? CenterId { get; set; }
        public Center? Center { get; set; }
    }
}
