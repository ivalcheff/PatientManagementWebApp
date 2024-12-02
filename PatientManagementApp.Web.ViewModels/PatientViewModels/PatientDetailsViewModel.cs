

using PatientManagementApp.Data.Models;
using static PatientManagementApp.Common.Enums;
using PatientManagementApp.Services.Mapping;

namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class PatientDetailsViewModel:IMapFrom<Patient>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? Email { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? DateOfBirth { get; set; }

        public string? TreatmentStartDate { get; set; }
        public string? TreatmentEndDate { get; set; }

        public string? ReasonForVisit { get; set; }

        public string? ReferredBy { get; set; }

        public string ImportantInfo { get; set; } = null!;
       
        public string? Diagnosis { get; set; }

        public PatientStatus Status { get; set; }
        public string? Feedback { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhoneNumber { get; set; }

        public string? EmergencyContactRelationship { get; set; }


        public List<FileUpload> Files { get; set; } = new List<FileUpload>();
        public List<PatientsMedications> PatientsMedications { get; set; } = new List<PatientsMedications>();

    }
}
