using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class PatientClinc
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public bool IsActive { get; set; }

    }
}
