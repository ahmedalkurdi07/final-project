using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    /*
     * المركز يضم اسم المركز والعنوان ورقم الهاتف وعنوان البريد 
     * وقائمة بالعيادات وقائمة المواعيد وقائمة الاطباء وقائمة المرضى وقائمة العاملين 
     */
    public class Center
    {
        [Key]
        public int Id { get; set; }           
        public required string Name { get; set; }          
        public required string PhoneNumber { get; set; } 
        public required string Email { get; set; }       
        public bool IsActive { get; set; }

        public required int AddressId { get; set; }
        public Address? Address { get; set; }

        public List<PatientCenter>? Patients = new List<PatientCenter>();

        public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
        public List<Appointment>? Appointments { get; set; } = new List<Appointment>();

    }
}
