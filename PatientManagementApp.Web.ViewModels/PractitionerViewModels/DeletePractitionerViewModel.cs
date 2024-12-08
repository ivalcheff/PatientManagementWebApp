

using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;

namespace PatientManagementApp.Web.ViewModels.PractitionerViewModels
{
    public class DeletePractitionerViewModel :IMapFrom<Practitioner>
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
