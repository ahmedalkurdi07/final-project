using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    /*
     * كلاس العنوان ويحوي بشكل اساسي المدينة والدولة والشارع 
     * ويضم قائمة بالموظفين وايضا قائمة بالاطباء وايضا قائمة بالمرضى 
     * ويضم قائمة بالمراكز
    */
    public  class Address
    {
        [Key]
        public int AddressId { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string StreetNumber { get; set; }
        public required string PostalCode { get; set; }
        public bool IsActive { get; set; }


        public List<Patient>? Patients { get; set; } = new List<Patient>();
        public List<Clinic>? Clinics { get; set; } = new List<Clinic>();
        public List<Center>? Centers { get; set; } = new List<Center>();
        public List<Doctor>? Doctors { get; set; } = new List<Doctor>();
        public List<Employee>? Employees { get; set; } = new List<Employee>();


    }
}
