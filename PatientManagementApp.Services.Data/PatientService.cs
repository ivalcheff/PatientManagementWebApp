
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.ViewModels.PatientViewModels;

namespace PatientManagementApp.Services.Data
{
    public class PatientService(IRepository<Patient, Guid> patientRepository
                              ,IRepository<Practitioner, Guid> practitionerRepository
                              ,IRepository<EmergencyContact, Guid> emergencyContactRepository
                              ,UserManager<ApplicationUser> userManager)
        : BaseService , IPatientService
    {
        private readonly IRepository<Patient, Guid> _patientRepository = patientRepository;
        private readonly IRepository<Practitioner, Guid> _practitionerRepository = practitionerRepository;
        private readonly IRepository<EmergencyContact, Guid> _emergencyContactRepository = emergencyContactRepository;
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


        public async Task<IEnumerable<PatientDetailsViewModel>> IndexAllPatientsAsync()
        {
            var patients = await this._patientRepository
                .GetAllAttached()
                .Where(p => p.IsActive)
                .AsNoTracking()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .To<PatientDetailsViewModel>()
                .ToListAsync();

            return patients;
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
            PatientDetailsViewModel? patient = await this._patientRepository
                .GetAllAttached()
                .Where(p => p.Id == id)
                .Include(p => p.EmergencyContact)
                .Include(p => p.PatientsMedications)
                .AsNoTracking()
                .To<PatientDetailsViewModel>()
                .FirstOrDefaultAsync();

            return patient;
        }


        public async Task<EditPatientViewModel?> GetEditPatientModelByIdAsync(Guid id)
        {
            EditPatientViewModel? model = await this._patientRepository
                .GetAllAttached()
                .Include(p => p.EmergencyContact)
                .Include(p =>p.Practitioner)
                .To<EditPatientViewModel>()
                .FirstOrDefaultAsync(p => p.Id == id);

            return model;
        }

        public async Task<bool> EditPatientAsync(EditPatientViewModel model)
        {
            if (!ValidateDate(model.TreatmentStartDate, DateFormatString, out DateTime treatmentStartDate))
            {
                return false;
            }

            if (!ValidateDate(model.DateOfBirth, DateFormatString, out DateTime dateOfBirth))
            {
                return false;
            }

            if (!ValidateDate(model.TreatmentEndDate, DateFormatString, out DateTime treatmentEndDate))
            {
                return false;
            }

            Patient editedPatient = AutoMapperConfig.MapperInstance.Map<Patient>(model);
            editedPatient.BirthDate = dateOfBirth;
            editedPatient.TreatmentStartDate = treatmentStartDate;
            editedPatient.TreatmentEndDate = treatmentEndDate;
            editedPatient.PractitionerId = model.PractitionerId;
            editedPatient.EmergencyContactId = model.EmergencyContactId;

            return await this._patientRepository.UpdateAsync(editedPatient);
        }

       

        public async Task<bool> AddNewPatientAsync(CreatePatientViewModel model, Guid userId)
        {
            if (!ValidateDate(model.TreatmentStartDate, DateFormatString, out DateTime treatmentStartDate))
            {
                return false;
            }

            if (!ValidateDate(model.DateOfBirth, DateFormatString, out DateTime dateOfBirth))
            {
                return false;
            }

            if (!ValidateDate(model.TreatmentEndDate, DateFormatString, out DateTime treatmentEndDate))
            {
                return false;
            }

            EmergencyContact emergencyContact = new EmergencyContact
            {
                Name = model.EmergencyContactName,
                PhoneNumber = model.EmergencyContactPhone,
                Relationship = model.EmergencyContactRelationship
            };

            Patient patient = new Patient();
            AutoMapperConfig.MapperInstance.Map(model, patient);
            patient.BirthDate = dateOfBirth;
            patient.TreatmentStartDate = treatmentStartDate;
            patient.TreatmentEndDate = treatmentEndDate;
            patient.EmergencyContact = emergencyContact;
            patient.PractitionerId = userId;
            patient.IsActive = true;

            await this._patientRepository.AddAsync(patient);
            return true;
        }


        public async Task<DeletePatientViewModel?> GetPatientDeleteModelAsync(Guid id)
        {
            DeletePatientViewModel? patientToDelete = await this._patientRepository
                .GetAllAttached()
                .Include(p=>p.EmergencyContact)
                .Include(p=> p.Appointments)
                .To<DeletePatientViewModel>()
                .FirstOrDefaultAsync(p => p.Id == id);

            return patientToDelete;
        }

        public async Task<bool> SoftDeletePatientAsync(Guid id)
        {
            Patient? patient = await this._patientRepository
                    .GetAllAttached()
                    .Include(p=>p.EmergencyContact)
                    .Include(p=> p.Appointments)
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return false;
            }

            patient.IsActive = false;

            if (patient.EmergencyContact != null)
            {
                patient.EmergencyContact.IsDeleted = true;
            }

            if (patient.Appointments != null && patient.Appointments.Any())
            {
                foreach (var appointment in patient.Appointments)
                {
                    appointment.IsDeleted = true;
                }
            }


            return await this._patientRepository.UpdateAsync(patient);
        }

    }
}
