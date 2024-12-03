﻿

using PatientManagementApp.Web.ViewModels.PatientViewModels;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDetailsViewModel>> IndexAllPatientsAsync();
        Task<IEnumerable<PatientDetailsViewModel>> IndexAllFromCurrentUser(Guid userId);

        Task<bool> AddNewPatientAsync(CreatePatientViewModel model, Guid userId);

        Task<PatientDetailsViewModel> GetPatientDetailsByIdAsync(Guid id);

        Task<EditPatientViewModel> EditPatientByIdAsync(Guid id, Guid userId);



        //List<SelectListItem> GetGenderOptions();
        //List<SelectListItem> GetStatusOptions();
    }
}
