using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class PatientCenter
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int CenterId { get; set; }
        public Center? Center { get; set; }
        public bool IsActive { get; set; }
    }
}
