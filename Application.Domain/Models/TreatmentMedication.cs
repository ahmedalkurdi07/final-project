using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class TreatmentMedication
    {
        [Key]
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public Treatment? Treatment { get; set; }

        public int MedicationId { get; set; }
        public Medication? Medication { get; set; }
    }
}
