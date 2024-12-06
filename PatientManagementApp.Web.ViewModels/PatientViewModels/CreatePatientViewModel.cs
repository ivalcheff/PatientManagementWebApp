
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.EmergencyContact;
using static PatientManagementApp.Common.EntityValidationMessages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;


namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class CreatePatientViewModel : IMapTo<Patient>, IHaveCustomMappings
    {
        public CreatePatientViewModel()
        {
            this.TreatmentStartDate = DateTime.UtcNow.ToString(DateFormatString);
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

        public string? TreatmentEndDate { get; set; }


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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreatePatientViewModel, Patient>()
                .ForMember(d => d.BirthDate, x => x.Ignore())
                .ForMember(d => d.TreatmentStartDate, x => x.Ignore())
                .ForMember(d=> d.TreatmentEndDate, x => x.Ignore())
                .ForMember(d => d.PractitionerId, x => x.Ignore());


        }
    }
}
