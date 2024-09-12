using Application.Domain.Enums;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class InvoiceForCreate
    {
        public required decimal Amount { get; set; }
        public required PaymentStatus Status { get; set; }
        public  bool IsActive { get; set; }

        public required int PatientId { get; set; }
        public required int EmployeeId { get; set; }

        public required int TreatmentId { get; set; }

        public required int AppointmentId { get; set; }

        public required int ClinicId { get; set; }

        public required int CenterId { get; set; }
    }
}
