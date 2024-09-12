using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class TreatmentMedicationForUpdate
    {
        public int? TreatmentId { get; set; }
        public Treatment? Treatment { get; set; }

        public int? MedicationId { get; set; }
        public Medication? Medication { get; set; }
    }
}
