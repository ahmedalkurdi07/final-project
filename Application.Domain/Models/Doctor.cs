using Application.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{   /*
     * كلاس الطبيب يحوي بشكل اساسي اسمه وتخصصه رقم الهاتف والايميل والجنس 
     * يحوي معرف المركز ومعرف العيادة 
     * وقائمة بمواعيد المرضى
    */
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Specialty Specialty { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public ConsultationHours ConsultationHours { get; set; }


        public required int AddressId { get; set; }
        public Address? Address { get; set; }

        public int ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

        public int CenterId { get; set; }
        public Center? Center { get; set; }

        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();


    }
}
