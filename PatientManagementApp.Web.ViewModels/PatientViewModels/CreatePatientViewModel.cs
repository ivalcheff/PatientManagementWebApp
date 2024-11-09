
using System.ComponentModel.DataAnnotations;

using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;


namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class CreatePatientViewModel
    {
        [Required]
        [MinLength(PatientFirstNameMinLength)]
        [MaxLength(PatientFirstNameMaxLength)]

        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(PatientLastNameMinLength)]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        [MinLength(EmailMinLength)]
        public string? Email { get; set; }

        [Required]
        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;


        [Required] 
        public string TreatmentStartDate { get; set; } = null!;

        public string? DateOfBirth { get; set; }

        [MinLength(ReasonForVisitMinLength)]
        [MaxLength(ReasonForVisitMaxLength)]
        public string? ReasonForVisit { get; set; }

        [MinLength(ReferredByMinLength)]
        [MaxLength(ReferredByMaxLength)]
        public string? ReferredBy { get; set; }

        [Required]
        [MinLength(PatientImportantInfoMinLength)]
        [MaxLength(PatientImportantInfoMaxLength)]
        public string ImportantInfo { get; set; } = null!;

        //Emergency contact info
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }

    }
}
