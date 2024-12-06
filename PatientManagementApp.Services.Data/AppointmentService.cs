
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Services.Mapping;
using Appointment = PatientManagementApp.Data.Models.Appointment;
using Patient = PatientManagementApp.Data.Models.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Services.Data
{

    public class AppointmentService(IRepository<Appointment, int> appointmentRepository 
                                    ,IRepository<Patient, Guid> patientRepository
                                    ,IRepository<Practitioner, Guid> practitionerRepository
                                    ,UserManager<ApplicationUser> userManager) 
        : BaseService, IAppointmentService
    {

        private readonly IRepository<Appointment, int> _appointmentRepository = appointmentRepository;
        private readonly IRepository<Practitioner, Guid> _practitionerRepository = practitionerRepository;
        private readonly IRepository<Patient, Guid> _patientRepository = patientRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;


        //INDEX
        public async Task<IEnumerable<AppointmentInfoViewModel>> IndexAllOrderedByDateAsync(Guid userId)
        {
            var appointments = await this._appointmentRepository
                .GetAllAttached()
                .Where(a => a.PractitionerId == userId)
                .AsNoTracking()
                .OrderBy(a => a.StartDate)
                .Include(a => a.Patient)
                .To<AppointmentInfoViewModel>()
                .ToListAsync();

            return appointments;
        }

        //DETAILS
        public async Task<AppointmentInfoViewModel> GetAppointmentDetailsByIdAsync(int id, Guid userId)
        {
            var appointment = await this._appointmentRepository
                .GetAllAttached()
                .Where(a => a.Id == id && a.PractitionerId == userId)
                .Include(a => a.Patient)
                .AsNoTracking()
                .To<AppointmentInfoViewModel>()
                .FirstOrDefaultAsync();

            return appointment;
        }


        //EDIT

        public async Task<EditAppointmentViewModel> GetEditAppointmentModelByIdAsync(int id, Guid userId)
        {
            EditAppointmentViewModel? model = await this._appointmentRepository
                .GetAllAttached()
                .Include(a => a.Patient)
                .To<EditAppointmentViewModel>()
                .FirstOrDefaultAsync(a => a.Id == id && a.PractitionerId == userId);

            return model;
        }

        public async Task<bool> EditAppointmentAsync(EditAppointmentViewModel model)
        {
            if (!ValidateDate(model.StartDate, AppointmentTimeFormat, out DateTime startTime))
            {
                return false;
            }
            if (!ValidateDate(model.EndDate, AppointmentTimeFormat, out DateTime endTime))
            {
                return false;
            }

            Appointment editedAppointment = AutoMapperConfig.MapperInstance.Map<Appointment>(model);
            editedAppointment.StartDate = startTime;
            editedAppointment.EndDate = endTime;
            editedAppointment.PatientId = model.PatientId;
            editedAppointment.PractitionerId = model.PractitionerId;

            return await this._appointmentRepository.UpdateAsync(editedAppointment);
        }


        //CREATE

        public async Task<bool> CreateNewAppointmentAsync(CreateAppointmentViewModel model)
        {
            if (model.PatientId == Guid.Empty)
            {
                throw new ArgumentException("Invalid PatientId");
            }

            if (!ValidateDate(model.StartDate, AppointmentTimeFormat, out DateTime appointmentStartDate))
            {
                return false;
            }
            if (!ValidateDate(model.EndDate, AppointmentTimeFormat, out DateTime appointmentEndDate))
            {
                return false;
            }

            Appointment appointment = new Appointment();
            
            AutoMapperConfig.MapperInstance.Map(model, appointment);
            appointment.StartDate = appointmentStartDate;
            appointment.EndDate = appointmentEndDate;

            await this._appointmentRepository.AddAsync(appointment);
            return true;
        }


        public async Task<IEnumerable<AppointmentInfoViewModel>> GetUpcomingAppointmentsForDayAsync(Guid userId, DayOfWeek day)
        {
            var startOfDay = DateTime.Today.AddDays((int)day - (int)DateTime.Today.DayOfWeek);
            var endOfDay = startOfDay.AddDays(1);

            var appointments = await this._appointmentRepository
                .GetAllAttached()
                .Where(a => a.PractitionerId == userId && a.StartDate >= startOfDay && a.StartDate < endOfDay)
                .AsNoTracking()
                .OrderBy(a => a.StartDate)
                .To<AppointmentInfoViewModel>()
                .ToListAsync();

            return appointments;
        }


        //GET PRACTITIONER
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


        //GET PATIENT
        public async Task<Patient?> GetPatientByNameAsync(string firstName, string lastName)
        {
            return await _patientRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
        }
    }
}
