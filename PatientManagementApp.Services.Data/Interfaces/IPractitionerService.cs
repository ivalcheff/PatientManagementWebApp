

using PatientManagementApp.Web.ViewModels.PractitionerViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IPractitionerService
    {
        Task<IEnumerable<PractitionerDetailsViewModel>>IndexAllPractitionersAsync();
        Task<PractitionerDetailsViewModel> GetPractitionerDetailsByIdAsync(Guid id);

        Task<PractitionerEditViewModel?> GetPractitionerEditModelByIdAsync(Guid id);
        Task<bool> EditPractitionerAsync(PractitionerEditViewModel model);
        Task<DeletePractitionerViewModel?> GetPractitionerDeleteModelAsync(Guid id);
        Task<bool> SoftDeletePractitionerAsync(Guid id);
    }
}
