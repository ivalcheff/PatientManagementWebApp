
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Services.Mapping;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using Appointment = PatientManagementApp.Data.Models.Appointment;
using Patient = PatientManagementApp.Data.Models.Patient;

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

      
        //CREATE

        public async Task CreateNewAppointmentAsync(CreateAppointmentViewModel model)
        {
            if (model.PatientId == Guid.Empty)
            {
                throw new ArgumentException("Invalid PatientId");
            }

            Appointment appointment = new Appointment();
            
            AutoMapperConfig.MapperInstance.Map(model, appointment);

            await this._appointmentRepository.AddAsync(appointment);

        }

        public async Task CreateNewAppointmentAsync(Appointment appointment)
        {
            await this._appointmentRepository.AddAsync(appointment);
        }



        //EDIT

        public Task<EditAppointmentViewModel> EditAppointmentByIdAsync(int id, Guid userId)
         {
            throw new NotImplementedException();
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
