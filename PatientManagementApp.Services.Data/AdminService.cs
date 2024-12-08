
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.ViewModels.Admin;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;

namespace PatientManagementApp.Services.Data
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Patient, Guid> _patientRepository;
        private readonly IRepository<Practitioner, Guid> _practitionerRepository;
        private readonly IRepository<Appointment, int> _appointmentRepository;

        public AdminService(
            IRepository<Patient, Guid> patientRepository,
            IRepository<Practitioner, Guid> practitionerRepository,
            IRepository<Appointment, int> appointmentRepository)
        {
            _patientRepository = patientRepository;
            _practitionerRepository = practitionerRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ManageViewModel> GetDeletedRecordsAsync()
        {
            var deletedPatients = await _patientRepository
                .GetAllAttached()
                .Where(p => !p.IsActive)
                .To<PatientDetailsViewModel>()
                .ToListAsync();

            var deletedPractitioners = await _practitionerRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted)
                .To<PractitionerDetailsViewModel>()
                .ToListAsync();

            var deletedAppointments = await _appointmentRepository
                .GetAllAttached()
                .Where(a => a.IsDeleted)
                .To<AppointmentInfoViewModel>()
                .ToListAsync();

            return new ManageViewModel
            {
                DeletedPatients = deletedPatients,
                DeletedPractitioners = deletedPractitioners,
                DeletedAppointments = deletedAppointments
            };
        }

        public async Task<bool> RestorePatientAsync(Guid id)
        {
            var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == id && !p.IsActive);
            if (patient == null) return false;

            patient.IsActive = true;
            if (patient.EmergencyContact != null)
            {
                patient.EmergencyContact.IsDeleted = false;
            }

            return await _patientRepository.UpdateAsync(patient);
        }

        public async Task<bool> RestorePractitionerAsync(Guid id)
        {
            var practitioner = await _practitionerRepository.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);
            if (practitioner == null) return false;

            practitioner.IsDeleted = false;
            return await _practitionerRepository.UpdateAsync(practitioner);
        }

        public async Task<bool> RestoreAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted);
            if (appointment == null) return false;

            appointment.IsDeleted = false;
            return await _appointmentRepository.UpdateAsync(appointment);
        }
    }

}
