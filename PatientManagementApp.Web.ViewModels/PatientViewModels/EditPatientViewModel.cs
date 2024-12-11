

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;
using static PatientManagementApp.Common.Enums;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.EmergencyContact;
using static PatientManagementApp.Common.EntityValidationMessages;

namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class EditPatientViewModel : IMapFrom<Patient>,IMapTo<Patient>, IHaveCustomMappings
    {

        public Guid Id { get; set; }
        public Guid PractitionerId { get; set; }
        public Guid EmergencyContactId { get; set; }

        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string? PractitionerFirstName { get; set; }


     
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string? PractitionerLastName { get; set; }

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

        [Required(ErrorMessage = DateFormatIsIncorrect)]
        public string TreatmentStartDate { get; set; } = null!;
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
        //TODO add Notes
        //TODO add Flies


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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Patient, EditPatientViewModel>()
                .ForMember(d => d.DateOfBirth,
                    x => x.MapFrom(s => s.BirthDate.ToString(DateFormatString)))
                .ForMember(d => d.TreatmentStartDate,
                    x => x.MapFrom(s => s.TreatmentStartDate.ToString(DateFormatString)))
                .ForMember(d => d.TreatmentEndDate,
                    x => x.MapFrom(s => s.TreatmentEndDate.ToString()));


            configuration.CreateMap<EditPatientViewModel, Patient>()
                .ForMember(d => d.BirthDate, x => x.Ignore())
                .ForMember(d => d.TreatmentStartDate, x => x.Ignore())
                .ForMember(d => d.TreatmentEndDate, x => x.Ignore())
                .ForMember(d => d.EmergencyContactId, x => x.Ignore())
                .ForMember(d => d.PractitionerId, x=> x.Ignore());
        }
    }
}
