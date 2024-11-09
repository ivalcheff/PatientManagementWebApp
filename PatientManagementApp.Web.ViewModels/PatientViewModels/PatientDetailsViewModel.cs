
using System.Reflection;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Common;
using static PatientManagementApp.Common.Enums;

namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class PatientDetailsViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public Enums.Gender Gender { get; set; }
        public string? Email { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? DateOfBirth { get; set; }

        public string? TreatmentStartDate { get; set; }
        public string? TreatmentEndDate { get; set; }

        public string? ReasonForVisit { get; set; }

        public string? ReferredBy { get; set; }

        public string ImportantInfo { get; set; } = null!;

        public PatientStatus Status { get; set; }
        public string? Feedback { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhoneNumber { get; set; }

        public string? EmergencyContactRelationship { get; set; }


        public List<FileUpload> Files { get; set; } = new List<FileUpload>();

        public List<PatientsDiagnoses> Diagnoses { get; set; } = new List<PatientsDiagnoses>();

        public List<PatientsMedications> PatientsMedications { get; set; } = new List<PatientsMedications>();

    }
}
