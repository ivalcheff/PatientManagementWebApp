
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Data.Models
{
    public class Practitioner
    {
        [Key, ForeignKey(nameof(User))]
        public Guid Id { get; set; } 
        public virtual ApplicationUser User { get; set; } = null!;

        [MaxLength(FirstNameMaxLength)]
        [Comment("Practitioner's first name")]
        public string? FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        [Comment("Practitioner's last name")]
        public string? LastName { get; set; } 

        [MaxLength(PhoneMaxLength)]
        [Comment("Practitioner's phone number")]
        public string? Phone { get; set; } 

        [Required]
        [Comment("Whether the practitioner has online consultations with patients")]
        public bool IsAvailableOnline { get; set; }

        [Comment("To implement soft-delete")]
        public bool IsDeleted { get; set; }

        [Required]
        [Comment("A list of all the patients for this practitioner")]
        public ICollection<Patient> Patients { get; set; } = 
            new HashSet<Patient>();

        [Required]
        [Comment("A list of the practitioner's specialties")]
        public ICollection<PractitionersSpecialties> PractitionersSpecialties { get; set; } =
            new HashSet<PractitionersSpecialties>();

        [Comment("The list of appointments")]
        public virtual ICollection<Appointment> Appointments { get; set; } =
            new HashSet<Appointment>();
    }
}
