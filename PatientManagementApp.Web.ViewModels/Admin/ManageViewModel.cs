

using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;

namespace PatientManagementApp.Web.ViewModels.Admin
{
    public class ManageViewModel
    {
        // List of deleted patients
        public IEnumerable<PatientDetailsViewModel> DeletedPatients { get; set; } = new List<PatientDetailsViewModel>();

        // List of deleted practitioners
        public IEnumerable<PractitionerDetailsViewModel> DeletedPractitioners { get; set; } = new List<PractitionerDetailsViewModel>();

        // List of deleted appointments
        public IEnumerable<AppointmentInfoViewModel> DeletedAppointments { get; set; } = new List<AppointmentInfoViewModel>();
    }
}
