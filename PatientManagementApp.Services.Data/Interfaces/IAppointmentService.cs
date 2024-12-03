

using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IAppointmentService 
    {
        Task<IEnumerable<AppointmentInfoViewModel>> IndexAllOrderedByDateAsync(Guid userId);
        Task<Practitioner?> GetPractitionerByUserIdAsync(Guid userId);

        Task<Practitioner?> GetPractitionerByIdAsync(Guid practitionerId);

        Task CreateNewAppointmentAsync(CreateAppointmentViewModel model);

        Task<AppointmentInfoViewModel> GetAppointmentDetailsByIdAsync(int id, Guid userId);

        Task<EditAppointmentViewModel> EditAppointmentByIdAsync(int id, Guid userId);
        Task<Patient?> GetPatientByNameAsync(string firstName, string lastName);

    }
}
