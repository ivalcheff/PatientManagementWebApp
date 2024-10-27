
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;

namespace PatientManagementApp.Data.Models
{
    public class Patient
    {
        [Key] 
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(PatientFirstNameMaxLength)]
        [Comment("Patient's first name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PatientLastNameMaxLength)]
        [Comment("Patient's last name")]
        public string LastName { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        [Comment("Patient's email address")]
        public string? Email { get; set; }


        [MaxLength(PhoneMaxLength)]
        [Comment("Patient's phone number")]
        public string? PhoneNumber { get; set; }

        [Comment("Patient's date of birth")]
        public DateTime BirthDate { get; set; }

        [Comment("Patient's age")]
        public int Age { get; set; }

        [Comment("Patient's gender")]
        public Gender Gender { get; set; }

        [Comment("The first date of the treatment")]
        public DateTime TreatmentStartDate { get; set; }

        [Comment("Final date of the treatment")]
        public DateTime TreatmentEndDate { get; set; }

        [Comment("Initial reason for visiting")]
        public string? ReasonForVisit { get; set; }

        [Comment("Who referred this patient")]
        public string? ReferredBy { get; set; }

        [Required]
        public Guid PractitionerId { get; set; }
        [ForeignKey(nameof(PractitionerId))]
        [Comment("The primary practitioner treating the patient")]
        public virtual Practitioner Practitioner { get; set; } = null!;

        public Guid EmergencyContactId { get; set; }
        [ForeignKey(nameof(EmergencyContactId))]
        [Comment("The patient's emergency contact")]
        public virtual EmergencyContact EmergencyContact { get; set; } = null!;

        [Required]
        [MaxLength(PatientImportantInfoMaxLength)]
        [Comment("Important information about the patient")]
        public string ImportantInfo { get; set; } = null!;

        [Required]
        [Comment("Whether the treatment is active in the moment")]
        public PatientStatus Status { get; set; }

        [Required]
        [Comment("The diagnoses of the patient")]
        public virtual ICollection<PatientsDiagnoses> PatientsDiagnoses { get; set; } = new List<PatientsDiagnoses>();

        [Required]
        [Comment("What medications is the patient currently on")]
        public virtual ICollection<PatientsMedications> PatientsMedications { get; set; } =
            new List<PatientsMedications>();

        [Required]
        [Comment("soft delete option")]
        public bool IsActive { get; set; }




    }
}
