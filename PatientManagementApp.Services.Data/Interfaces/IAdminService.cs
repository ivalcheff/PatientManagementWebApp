

using PatientManagementApp.Web.ViewModels.Admin;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IAdminService
    {
        Task<ManageViewModel> GetDeletedRecordsAsync();
        Task<bool> RestorePatientAsync(Guid id);
        Task<bool> RestorePractitionerAsync(Guid id);
        Task<bool> RestoreAppointmentAsync(int id);



    }
}
