

using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Services.Data.Interfaces
{
    public interface IServiceBase
    {
        Task<Practitioner?> GetPractitionerByUserIdAsync(Guid userId);

        Task<Practitioner?> GetPractitionerByIdAsync(Guid practitionerId);


       
    }
}
