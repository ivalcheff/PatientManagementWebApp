
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Practitioner;


namespace PatientManagementApp.Data.Models
{
    public class Practitioner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to link Practitioner to ApplicationUser
        public Guid UserId { get; set; } 
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(PractitionerFirstNameMaxLength)]
        [Comment("Practitioner's first name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PractitionerLastNameMaxLength)]
        [Comment("Practitioner's last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("Practitioner's phone number")]
        public string Phone { get; set; } = null!;

        [Required]
        [Comment("Whether the practitioner has online consultations with patients")]
        public bool IsAvailableOnline { get; set; }

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
