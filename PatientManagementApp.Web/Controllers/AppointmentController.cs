
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

namespace PatientManagementApp.Web.Controllers
{
    [Authorize]

    public class AppointmentController(ApplicationDbContext dbContext, 
                                        UserManager<ApplicationUser> userManager,
                                        IAppointmentService appointmentService) 
        : BaseController
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IAppointmentService _appointmentService = appointmentService;

        //INDEX

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            var (appointments, totalCount) = await this._appointmentService.IndexAllOrderedByDateAsync(userId, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(appointments);
        }

        //DETAILS

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            
            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            AppointmentInfoViewModel appointment = await _appointmentService
                .GetAppointmentDetailsByIdAsync(id, userId);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        //CREATE

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Details", "Practitioner");
            }

            var practitioner = await _appointmentService.GetPractitionerByIdAsync(user.Id);

            if (practitioner == null)
            {
                ModelState.AddModelError("", "You must be registered as a practitioner to create an appointment.");
                return RedirectToAction(nameof(Index));
            }

            var model = new CreateAppointmentViewModel()
            {
                PractitionerId = practitioner.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.View(model);
                }

                // Lookup practitioner in the database
                var practitioner = await _appointmentService
                    .GetPractitionerByIdAsync(model.PractitionerId);
                if (practitioner == null)
                {
                    ModelState.AddModelError("", "The specified practitioner does not exist.");
                    return View(model);
                }

                // Lookup the patient by first name and last name
                var patient = await _appointmentService
                    .GetPatientByNameAsync(model.PatientFirstName, model.PatientLastName);
                if (patient == null)
                {
                    ModelState.AddModelError("", "The specified patient could not be found.");
                    return View(model);
                }

                model.PatientId = patient.Id;

                bool result = await this._appointmentService.CreateNewAppointmentAsync(model);

                if (!result)
                {
                    this.ModelState.AddModelError(nameof(model.StartDate), $"The date should be in the following format: {AppointmentTimeFormat}");
                    return this.View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", "An error occurred while creating the appointment.");
                return View(model);
            }
        }

        //EDIT

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            Guid userId = user!.Id;

            EditAppointmentViewModel? model = await this._appointmentService
                .GetEditAppointmentModelByIdAsync(id, userId);

            if (model == null)
            {
                return this.RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAppointmentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                bool isUpdated = await this._appointmentService.EditAppointmentAsync(model);
                if (!isUpdated)
                {
                    return this.View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the appointment.");

                return View(model);
            }
        }

        //CALENDAR
       [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            if (userId == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var appointments = await _dbContext.Appointments
                .Where(a => a.PractitionerId == userId)
                .Include(a => a.Patient)
                .ToListAsync();

            var events = appointments.Select(a => new
            {
                title = $"{a.Patient.FirstName} {a.Patient.LastName} - {a.Description}",
                start = a.StartDate.ToString(AppointmentTimeFormat), // ISO format compatible with FullCalendar
                end = a.EndDate.ToString(AppointmentTimeFormat),
                id = a.Id.ToString()
            });

            return new JsonResult(events);
        }


        //UPCOMING APPOINTMENTS PARTIAL VIEW
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsForDay(DayOfWeek day)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = user.Id;

            // Ensure the day is within the valid range (0 - 6).
            if (!Enum.IsDefined(typeof(DayOfWeek), day))
            {
                return BadRequest("Invalid day of the week.");
            }

            var appointments = await _appointmentService.GetUpcomingAppointmentsForDayAsync(userId, day);

            ViewBag.CurrentDayIndex = (int)day;
            ViewBag.HasPreviousDay = day > DayOfWeek.Monday;
            ViewBag.HasNextDay = day < DayOfWeek.Sunday;

            return PartialView("_UpcomingAppointmentsPartial", appointments ?? new List<AppointmentInfoViewModel>());
        }


        //SOFT DELETE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AppointmentInfoViewModel? model = await this._appointmentService
                .GetAppointmentModelForDeleteAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteConfirmed(AppointmentInfoViewModel model)
        {
            bool isDeleted = await this._appointmentService
                .SoftDeleteAppointmentAsync(model.Id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Unexpected error occured. Could not delete appointment.";
                return this.RedirectToAction(nameof(Delete), new { id = model.Id });
            }

            return this.RedirectToAction(nameof(Index));

        }

    }
}
