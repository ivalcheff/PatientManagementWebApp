using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Data;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]

    public class PractitionerController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) 
        : BaseController
    {

        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            var user = await _userManager.GetUserAsync(User);
            id = user?.Id;

            Practitioner? practitioner = await this._dbContext
                .Practitioners
                .Where(p => p.Id == id)
                .Include(p => p.PractitionersSpecialties)
                .ThenInclude(ps => ps.Specialty)
                .Include(p => p.Appointments)
                .ThenInclude(appointment => appointment.Patient)
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
                Specialties = practitioner.PractitionersSpecialties
                    .Select(ps => ps.Specialty.Name).ToList(),
                Appointments = practitioner.Appointments
                    .Select(a => new AppointmentInfoViewModel
                    {
                        Id = a.Id,
                        Description = a.Description,
                        StartDate = a.StartDate.ToString(AppointmentTimeFormat),
                        EndDate = a.EndDate.ToString(AppointmentTimeFormat),
                        PatientFirstName = a.Patient.FirstName,
                        PatientLastName = a.Patient.LastName
                    }).ToList()

            };


            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var specialties = await _dbContext.Specialties.ToListAsync();

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
                    SelectedSpecialties = p.PractitionersSpecialties
                        .Select(ps => ps.SpecialtyId).ToList(),


                    Appointments = p.Appointments
                        .Select(a => new AppointmentInfoViewModel()
                        {
                            Id = a.Id, 
                            Description = a.Description,
                            StartDate = a.StartDate.ToString(DateFormatString),
                            EndDate = a.EndDate.ToString(DateFormatString),
                            PatientFirstName = a.Patient.FirstName,
                            PatientLastName = a.Patient.LastName
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (practitioner == null)
            {
                return NotFound();
            }

            practitioner.Specialties = specialties
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = practitioner.SelectedSpecialties.Contains(s.Id)
                }).ToList();


            return View(practitioner);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PractitionerEditViewModel model, Guid id)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Practitioner? p = await _dbContext
                .Practitioners
                .Include(p => p.PractitionersSpecialties)
                .Include(practitioner => practitioner.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                return NotFound();
            }

            p.FirstName = model.FirstName;
            p.LastName = model.LastName;
            p.Phone = model.Phone;
            p.User.Email = model.Email;
            p.IsAvailableOnline = model.IsAvailableOnline;

            // Update specialties
           // var currentSpecialties = p.PractitionersSpecialties.ToList();
           // _dbContext.PractitionersSpecialties.RemoveRange(currentSpecialties);
            foreach (var specialtyId in model.SelectedSpecialties)
            {
                p.PractitionersSpecialties.Add(new PractitionersSpecialties
                {
                    PractitionerId = p.Id,
                    SpecialtyId = specialtyId
                });
            }


            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details));

        }
    }
}
