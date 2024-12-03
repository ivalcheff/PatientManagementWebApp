

using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;


namespace PatientManagementApp.Web.ViewModels.PractitionerViewModels
{
    public class PractitionerDetailsViewModel
    {
        public Guid Id { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsAvailableOnline { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

        public List<AppointmentInfoViewModel> Appointments { get; set; } = new List<AppointmentInfoViewModel>();

        public List<string> Specialties { get; set; } = new();

    }
}
