using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class MedicationForCreate
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        /*public bool IsActive { get; set; }*/
    }
}
