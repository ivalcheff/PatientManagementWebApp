

using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IAppointmentService : IServiceBase
    {
        Task<IEnumerable<AppointmentInfoViewModel>> IndexAllOrderedByDateAsync(Guid userId);
       

        Task CreateNewAppointmentAsync(CreateAppointmentViewModel model);

        Task<AppointmentInfoViewModel> GetAppointmentDetailsByIdAsync(int id, Guid userId);

        Task<EditAppointmentViewModel> EditAppointmentByIdAsync(int id, Guid userId);
        Task<Patient?> GetPatientByNameAsync(string firstName, string lastName);

    }
}
