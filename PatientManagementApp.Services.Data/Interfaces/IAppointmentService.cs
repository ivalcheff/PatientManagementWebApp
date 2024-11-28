

using PatientManagementApp.Web.ViewModels.AppointmentViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentInfoViewModel>> IndexAllOrderedByDateAsync(Guid userId);
       

        Task CreateNewAppointmentAsync(CreateAppointmentViewModel model, Guid userId);

        Task<AppointmentInfoViewModel> GetAppointmentDetailsByIdAsync(int id, Guid userId);

        Task<EditAppointmentViewModel> EditAppointmentByIdAsync(int id, Guid userId);


    }
}
