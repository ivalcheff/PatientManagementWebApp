
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


        //TODO: implement transfer to another Practitioner


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


        //INDEX
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


        //DETAILS
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


        //EDIT
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

            DateTime? treatmentEndDate = null;
            if (!string.IsNullOrWhiteSpace(model.TreatmentEndDate))
            {
                if (!ValidateDate(model.TreatmentEndDate, DateFormatString, out DateTime tempTreatmentEndDate))
                {
                    return false;
                }
                treatmentEndDate = tempTreatmentEndDate;
            }

            var patient = await _patientRepository
                .GetAllAttached()
                .Include(p=>p.EmergencyContact)
                .Include(p => p.Practitioner)
                .FirstOrDefaultAsync(p => p.Id == model.Id);
           
            if (patient == null)
            {
                return false; // Patient does not exist
            }

            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.BirthDate = dateOfBirth;
            patient.TreatmentStartDate = treatmentStartDate;
            patient.TreatmentEndDate = treatmentEndDate;
            patient.Gender = model.Gender;
            patient.Status = model.Status;
            patient.PhoneNumber = model.PhoneNumber;
            patient.ImportantInfo = model.ImportantInfo;
            patient.Diagnosis = model.Diagnosis;
            patient.ReasonForVisit = model.ReasonForVisit;
            patient.ReferredBy = model.ReferredBy;
            patient.PractitionerId = model.PractitionerId;
            patient.EmergencyContactId = model.EmergencyContactId;

            if (patient.EmergencyContact != null)
            {
                patient.EmergencyContact.Name = model.EmergencyContactName;
                patient.EmergencyContact.PhoneNumber = model.EmergencyContactPhoneNumber;
                patient.EmergencyContact.Relationship = model.EmergencyContactRelationship;
            }


            return await this._patientRepository.UpdateAsync(patient);
        }

       //ADD NEW PATIENT

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

        //SOFT-DELETE
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

            //clear all linked entities from their associated data:

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

            if (patient.Notes != null && patient.Notes.Any())
            {
                foreach (var note in patient.Notes)
                {
                    note.IsDeleted = true;
                }
            }

            if (patient.Files != null && patient.Files.Any())
            {
                foreach (var file in patient.Files)
                {
                    file.IsDeleted = true;
                }
            }

            if (patient.PatientsMedications != null && patient.PatientsMedications.Any())
            {
                patient.PatientsMedications.Clear();
            }

            return await this._patientRepository.UpdateAsync(patient);
        }

    }
}
