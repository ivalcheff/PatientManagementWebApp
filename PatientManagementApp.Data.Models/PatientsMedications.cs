
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PatientManagementApp.Data.Models
{
    [PrimaryKey(nameof(PatientId), nameof(MedicationId))]
    public class PatientsMedications
    {
        public Guid PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!;

        public Guid MedicationId { get; set; }

        [ForeignKey(nameof(MedicationId))]
        public Medication Medication { get; set; } = null!;
    }
}
