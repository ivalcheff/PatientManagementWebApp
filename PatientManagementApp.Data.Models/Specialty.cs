
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Specialty;

namespace PatientManagementApp.Data.Models
{
    public class Specialty
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(SpecialtyNameMaxLength)]
        [Comment("The specialty name")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("a list of all practitioners with this specialty")]
        public virtual ICollection<PractitionersSpecialties> PractitionersSpecialties { get; set; }
            = new HashSet<PractitionersSpecialties>();

    }
}
