
using System.ComponentModel.DataAnnotations;
using PatientManagementApp.Common;
using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.EmergencyContact;
using static PatientManagementApp.Common.EntityValidationMessages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class CreatePatientViewModel
    {
        public CreatePatientViewModel()
        {
            this.TreatmentStartDate = DateTime.Now.ToString(DateFormat);
        }

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
       
        [Required] 
        public string TreatmentStartDate { get; set; }


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

        [MinLength(DiagnosisMinLength)]
        [MaxLength(DiagnosisMaxLength)]
        public string? Diagnosis { get; set; } 


        [Required]
        public PatientStatus Status { get; set; }

        public List<SelectListItem> PatientStatusOptions { get; set; } = new();

        //Emergency contact info
        [MinLength(EmergencyContactNameMinLength)]
        [MaxLength(EmergencyContactNameMaxLength)]
        public string? EmergencyContactName { get; set; }

        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string? EmergencyContactPhone { get; set; }

        [MinLength(EmergencyContactRelationshipMinLength)]
        [MaxLength(EmergencyContactRelationshipMaxLength)]
        public string? EmergencyContactRelationship { get; set; }

    }
}
