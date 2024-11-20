

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.EmergencyContact;
using static PatientManagementApp.Common.EntityValidationMessages;

namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class EditPatientViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = FirstNameIsRequired)]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]

        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = LastNameIsRequired)]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        [MinLength(EmailMinLength)]
        public string? Email { get; set; }

        [Required(ErrorMessage = PhoneIsRequired)]
        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public string? DateOfBirth { get; set; }

        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        
        public List<SelectListItem> GenderOptions { get; set; } = new();


        //Treatment info
        public string? TreatmentStartDate { get; set; }
        public string? TreatmentEndDate { get; set; }

        public PatientStatus Status { get; set; }
        public List<SelectListItem> PatientStatusOptions { get; set; } = new();

        [MinLength(ReasonForVisitMinLength)]
        [MaxLength(ReasonForVisitMaxLength)]
        public string? ReasonForVisit { get; set; }

        [MinLength(DiagnosisMinLength)]
        [MaxLength(DiagnosisMaxLength)]
        public string? Diagnosis { get; set; }


        //TODO add Medications


        [MinLength(ReferredByMinLength)]
        [MaxLength(ReferredByMaxLength)]
        public string? ReferredBy { get; set; }

        [Required]
        [MinLength(PatientImportantInfoMinLength)]
        [MaxLength(PatientImportantInfoMaxLength)]
        public string ImportantInfo { get; set; } = null!;

        [MinLength(FeedbackMinLength)]
        [MaxLength(FeedbackMaxLength)]
        public string? Feedback { get; set; }

        //Emergency contact info
        [MinLength(EmergencyContactNameMinLength)]
        [MaxLength(EmergencyContactNameMaxLength)]
        public string? EmergencyContactName { get; set; }

        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string? EmergencyContactPhoneNumber { get; set; }

        [MinLength(EmergencyContactRelationshipMinLength)]
        [MaxLength(EmergencyContactRelationshipMaxLength)]
        public string? EmergencyContactRelationship { get; set; }
    }
}
