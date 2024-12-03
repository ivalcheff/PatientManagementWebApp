﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.Enums;
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


        public async Task<EditPatientViewModel> EditPatientByIdAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();

            //    Patient? patient = await this._patientRepository
            //        .GetAllAttached()
            //        .Include(p => p.EmergencyContact)
            //        .FirstOrDefaultAsync(p => p.Id == id);

            //    patient.FirstName = model.FirstName;
            //    patient.LastName = model.LastName;
            //    patient.Email = model.Email;
            //    patient.PhoneNumber = model.PhoneNumber;
            //    patient.Age = model.Age;
            //    patient.Gender = model.Gender;
            //    patient.BirthDate = dateOfBirth;
            //    patient.TreatmentStartDate = treatmentStartDate;
            //    patient.TreatmentEndDate = treatmentEndDate;
            //    patient.ReasonForVisit = model.ReasonForVisit;
            //    patient.ReferredBy = model.ReferredBy;
            //    patient.ImportantInfo = model.ImportantInfo;
            //    patient.Status = model.Status;
            //    patient.Feedback = model.Feedback;
            //    patient.EmergencyContact.Name = model.EmergencyContactName;
            //    patient.EmergencyContact.PhoneNumber = model.EmergencyContactPhoneNumber;
            //    patient.EmergencyContact.Relationship = model.EmergencyContactRelationship;


            //    await this._patientRepository.UpdateAsync(patient);
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

        


        /*
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
        */
    }
}
