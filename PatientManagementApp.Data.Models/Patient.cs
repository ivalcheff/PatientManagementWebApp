
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        public string? Email { get; set; }

        [MaxLength(PhoneMaxLength)]
        public string? PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        
        public DateTime TreatmentStartDate { get; set; }
        public DateTime TreatmentEndDate { get; set; }

        public string? ReasonForVisit { get; set; } 
        public string? ReferredBy { get; set; }

        [Required]
        public Guid PractitionerId { get; set; }
        [ForeignKey(nameof(PractitionerId))] 
        public virtual Practitioner Practitioner { get; set; } = null!;

        public Guid EmergencyContactId { get; set; }
        [ForeignKey(nameof(EmergencyContactId))]
        public virtual EmergencyContact EmergencyContact { get; set; } = null!;

        public string ImportantInfo { get; set; } = null!;
        public PatientStatus Status { get; set; }

        public virtual ICollection<PatientsDiagnoses> PatientsDiagnoses { get; set; } = new List<PatientsDiagnoses>();

        public virtual ICollection<PatientsMedications> PatientsMedications { get; set; } =
            new List<PatientsMedications>();
        public bool IsActive { get; set; }




    }
}
