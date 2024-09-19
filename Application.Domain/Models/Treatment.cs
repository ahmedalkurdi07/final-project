using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{/*  راشيتة دوا*/
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        public required DateTime TreatmentDate { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }


        public Doctor? Doctor { get; set; }
        public int DoctorId { get; set; }

        public Clinic? Clinic { get; set; }
        public int ClinicId { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public List<TreatmentMedication>? TreatmentMedications { get; set; } = new List<TreatmentMedication>();


    }
}
