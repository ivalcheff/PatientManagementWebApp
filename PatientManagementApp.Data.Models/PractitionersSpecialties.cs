

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PatientManagementApp.Data.Models
{
    [PrimaryKey(nameof(PractitionerId), nameof(SpecialtyId))]
    public class PractitionersSpecialties
    {
        public Guid PractitionerId { get; set; }
        [ForeignKey(nameof(PractitionerId))]
        public Practitioner Practitioner { get; set; } = null!;

        public Guid SpecialtyId { get; set; }
        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; } = null!;
    }
}
