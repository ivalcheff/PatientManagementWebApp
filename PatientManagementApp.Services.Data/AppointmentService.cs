
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Services.Mapping;

namespace PatientManagementApp.Services.Data
{

    public class AppointmentService(IRepository<Appointment, int> appointmentRepository, UserManager<ApplicationUser> userManager) 
        : IAppointmentService
    {

        private readonly IRepository<Appointment, int> _appointmentRepository = appointmentRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<IEnumerable<AppointmentInfoViewModel>> IndexAllOrderedByDateAsync(Guid userId)
        {
            var appointments = await this._appointmentRepository
                .GetAllAttached()
                .Where(a => a.PractitionerId == userId)
                .AsNoTracking()
                .OrderByDescending(a => a.StartDate)
                .To<AppointmentInfoViewModel>()
                .ToListAsync();

            return appointments;
        }


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

        public Task<EditAppointmentViewModel> EditAppointmentByIdAsync(int id, Guid userId)
        {
            throw new NotImplementedException();
        }


        public Task CreateNewAppointmentAsync(CreateAppointmentViewModel model, Guid userId)
        {
            throw new NotImplementedException();
        }
       
    }
}
