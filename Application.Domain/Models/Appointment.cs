using Application.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    /*
     * كلاس الموعد ويحوي بشكل اساسي تاريخ الموعد 
     * وحالة الموعد تكون اما مؤكد او ملغي 
     * ايضا تحوي معرف كل مايلي 
     * - المريض والطبيب والمركز والعيادة 
     * وتضم معرف الفاتورة معها 
     */
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public bool IsActive { get; set; }

        public required int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public required int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int CenterId { get; set; }
        public Center? Center { get; set; }

        public int ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

    }
}
