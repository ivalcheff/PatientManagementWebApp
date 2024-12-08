

using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;

namespace PatientManagementApp.Web.ViewModels.PatientViewModels
{
    public class DeletePatientViewModel : IMapFrom<Patient>
    {
        public Guid Id { get; set; } 

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
