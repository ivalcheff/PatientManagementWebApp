using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]

    public class PractitionerController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) 
        : BaseController
    {

        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            Practitioner? practitioner = await this._dbContext
                .Practitioners
                .Where(p => p.Id == id)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.Appointments)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.User)
                .Include(p => p.Patients)
                .FirstOrDefaultAsync();

            if (practitioner == null)
            {
                return NotFound();
            }

            var model = new PractitionerDetailsViewModel()
            {
                Id = practitioner.Id,
                FirstName = practitioner.FirstName,
                LastName = practitioner.LastName,
                Phone = practitioner.Phone,
                Email = practitioner.User.Email,
                Patients = practitioner.Patients,
                //Specialties = practitioner.PractitionersSpecialties.Select

            };


            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var practitioner = await _dbContext
                .Practitioners
                .Where(p => p.Id == id)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.User)
                .Include(p => p.Appointments)
                .AsNoTracking()
                .Select(p => new PractitionerEditViewModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Email = p.User.Email,
                    Patients = p.Patients,
                    IsAvailableOnline = p.IsAvailableOnline,
                    Appointments = p.Appointments
                        .Select(a => new AppointmentInfoViewModel()
                        {
                            Id = a.Id, 
                            Description = a.Description,
                            StartDate = a.StartDate.ToString(DateFormat),
                            EndDate = a.EndDate.ToString(DateFormat),
                            PatientFirstName = a.Patient.FirstName,
                            PatientLastName = a.Patient.LastName
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return View(practitioner);
        }

    }
}
