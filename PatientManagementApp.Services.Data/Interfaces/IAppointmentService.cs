

using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IAppointmentService
    {
        Task<(IEnumerable<AppointmentInfoViewModel> Appointments, int TotalCount)> IndexAllOrderedByDateAsync(
            Guid userId, int pageNumber, int pageSize);
        Task<IEnumerable<AppointmentInfoViewModel>> GetUpcomingAppointmentsForDayAsync(Guid userId, DayOfWeek day);
        Task<Practitioner?> GetPractitionerByUserIdAsync(Guid userId);

        Task<Practitioner?> GetPractitionerByIdAsync(Guid practitionerId);

        Task<bool> CreateNewAppointmentAsync(CreateAppointmentViewModel model);

        Task<AppointmentInfoViewModel> GetAppointmentDetailsByIdAsync(int id, Guid userId);

        Task<EditAppointmentViewModel> GetEditAppointmentModelByIdAsync(int id, Guid userId);

        Task<bool> EditAppointmentAsync(EditAppointmentViewModel model);
        Task<Patient?> GetPatientByNameAsync(string firstName, string lastName);

        Task<AppointmentInfoViewModel?> GetAppointmentModelForDeleteAsync(int id);
        Task<bool> SoftDeleteAppointmentAsync(int id);


    }
}
