

using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Web.ViewModels.PatientViewModels;


namespace PatientManagementApp.Web.ViewModels.PractitionerViewModels
{
    public class PractitionerDetailsViewModel : IMapFrom<Practitioner>
    {
        public Guid Id { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsAvailableOnline { get; set; }

        public List<PatientDetailsViewModel> Patients { get; set; } = new List<PatientDetailsViewModel>();

        public List<AppointmentInfoViewModel> Appointments { get; set; } = new List<AppointmentInfoViewModel>();

        public List<string> Specialties { get; set; } = new();

    }
}
