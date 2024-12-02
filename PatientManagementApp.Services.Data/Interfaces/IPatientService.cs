

using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManagementApp.Web.ViewModels.PatientViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IPatientService : IServiceBase
    {
        Task<IEnumerable<PatientDetailsViewModel>> IndexAllFromCurrentUser(Guid userId);

        Task AddNewPatientAsync(CreatePatientViewModel model, DateTime dateOfBirth, DateTime treatmentStartDate, Guid userId);

        Task<PatientDetailsViewModel> GetPatientDetailsByIdAsync(Guid id);

        Task<EditPatientViewModel> EditPatientByIdAsync(Guid id);

        List<SelectListItem> GetGenderOptions();
        List<SelectListItem> GetStatusOptions();
    }
}
