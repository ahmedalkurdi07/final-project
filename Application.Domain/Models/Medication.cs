using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }

        public List<TreatmentMedication>? TreatmentMedications { get; set; } = new List<TreatmentMedication>();


    }
}
