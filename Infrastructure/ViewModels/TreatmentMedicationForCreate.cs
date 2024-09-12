using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class TreatmentMedicationForCreate
    {
        public required int TreatmentId { get; set; }
        public required int MedicationId { get; set; }
    }
}
