
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.Enums;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.ViewModels.PatientViewModels;

namespace PatientManagementApp.Services.Data
{
    public class PatientService(IRepository<Patient, Guid> patientRepository
                              ,IRepository<Practitioner, Guid> practitionerRepository
                              ,UserManager<ApplicationUser> userManager) : IPatientService
    {

        private readonly IRepository<Patient, Guid> _patientRepository = patientRepository;
        private readonly IRepository<Practitioner, Guid> _practitionerRepository = practitionerRepository;

        private readonly UserManager<ApplicationUser> _userManager = userManager;


        public async Task<Practitioner?> GetPractitionerByUserIdAsync(Guid userId)
        {
            return await _practitionerRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(p => p.Id == userId);
        }

        public async Task<Practitioner?> GetPractitionerByIdAsync(Guid practitionerId)
        {
            return await _practitionerRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(p => p.Id == practitionerId);
        }


        public async Task<IEnumerable<PatientDetailsViewModel>> IndexAllFromCurrentUser(Guid userId)
        {
            var patients = await this._patientRepository
                .GetAllAttached()
                .Where(p => p.IsActive)
                .Where(p => p.PractitionerId == userId)
                .AsNoTracking()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .To<PatientDetailsViewModel>()
                .ToListAsync();

            return patients;
        }

        public async Task<PatientDetailsViewModel> GetPatientDetailsByIdAsync(Guid id)
        {
            var patient = await this._patientRepository
                .GetAllAttached()
                .Where(p => p.Id == id)
                .Include(p => p.EmergencyContact)
                .Include(p => p.PatientsMedications)
                .AsNoTracking()
                .To<PatientDetailsViewModel>()
                .FirstOrDefaultAsync();

            return patient;
        }

        public async Task AddNewPatientAsync(CreatePatientViewModel model, DateTime dateOfBirth, DateTime treatmentStartDate, Guid userId)
        {
            Patient patient = new Patient()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                BirthDate = dateOfBirth,
                Age = model.Age,
                Gender = model.Gender,
                Diagnosis = model.Diagnosis,
                TreatmentStartDate = treatmentStartDate,
                ReasonForVisit = model.ReasonForVisit,
                ReferredBy = model.ReferredBy,
                ImportantInfo = model.ImportantInfo,
                EmergencyContact = new EmergencyContact()
                {
                    Name = model.EmergencyContactName,
                    PhoneNumber = model.EmergencyContactPhone,
                    Relationship = model.EmergencyContactRelationship
                },
                Status = model.Status,
                IsActive = true,
                PractitionerId = (Guid)userId,

            };

            await this._patientRepository.AddAsync(patient);

        }

        

        public async Task<EditPatientViewModel> EditPatientByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> GetGenderOptions()
        {
            var gendersList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                })
                .ToList();

            return gendersList;
        }

        public List<SelectListItem> GetStatusOptions()
        {
            var patientStatusList = Enum.GetValues(typeof(PatientStatus))
                .Cast<PatientStatus>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                })
                .ToList();

            return patientStatusList;
        }
    }
}
