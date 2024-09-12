using Application.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public required Gender Gender { get; set; }

        public required int AddressId { get; set; }
        public Address? Address { get; set; }

        public int ClinicId {  get; set; }
        public Clinic? Clinic {  get; set; }

        public int CenterId { get; set; }
        public Center? Center { get; set; }
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();

    }
}
