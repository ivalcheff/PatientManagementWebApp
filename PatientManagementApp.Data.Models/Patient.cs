
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

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("Patient's phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Comment("The patient's email address")]
        [MaxLength(EmailMaxLength)]
        public string? Email { get; set; }


        [Comment("Patient's date of birth")]
        public DateTime BirthDate { get; set; }


        [Comment("Patient's age")]
        public int Age { get; set; }


        [Comment("Patient's gender")]
        public Gender Gender { get; set; }

        [Required]
        [Comment("The first date of the treatment")]
        public DateTime TreatmentStartDate { get; set; }


        [Comment("Final date of the treatment")]
        public DateTime TreatmentEndDate { get; set; }


        [Comment("Initial reason for visiting")]
        public string? ReasonForVisit { get; set; }


        [Comment("Who referred this patient")]
        public string? ReferredBy { get; set; }

        [Comment("Feedback from patient regarding the treatment")]
        public string? Feedback { get; set; }
        
        [Required]
        [MaxLength(PatientImportantInfoMaxLength)]
        [Comment("Important information about the patient")]
        public string ImportantInfo { get; set; } = null!;


        [Required]
        [Comment("Whether the treatment is active in the moment")]
        public PatientStatus Status { get; set; }


        [Required]
        [Comment("soft delete option")]
        public bool IsActive { get; set; }

        //navigation properties

        [Required]
        public Guid PractitionerId { get; set; }

        [ForeignKey(nameof(PractitionerId))]
        [Comment("The primary practitioner treating the patient")]
        public virtual Practitioner Practitioner { get; set; } = null!;


        public Guid EmergencyContactId { get; set; }

        [ForeignKey(nameof(EmergencyContactId))]
        [Comment("The patient's emergency contact")]
        public virtual EmergencyContact EmergencyContact { get; set; } = null!;


        //collections

        [Comment("The list of appointments")]
        public virtual ICollection<Appointment> Appointments { get; set; } =
            new HashSet<Appointment>();


        [Comment("A collection of files on the patient")]
        public virtual ICollection<FileUpload> Files { get; set; } =
            new HashSet<FileUpload>();

        [Comment("A collection of notes about the patient")]
        public virtual ICollection<Note> Notes { get; set; } =
            new HashSet<Note>();


        //mapping tables 

        [Required]
        [Comment("The diagnoses of the patient")]
        public virtual ICollection<PatientsDiagnoses> PatientsDiagnoses { get; set; } = 
            new HashSet<PatientsDiagnoses>();


        [Required]
        [Comment("What medications is the patient currently on")]
        public virtual ICollection<PatientsMedications> PatientsMedications { get; set; } =
            new HashSet<PatientsMedications>();


        
    }
}
