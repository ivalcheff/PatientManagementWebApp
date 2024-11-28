using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // GET: AppointmentController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
           var user = await _userManager.GetUserAsync(User);
           var userId = user!.Id;
            

           IEnumerable<AppointmentInfoViewModel> appointments
               = await this._appointmentService
                   .IndexAllOrderedByDateAsync(userId);

           return View(appointments);
        }



        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            var appointment = await _appointmentService
                .GetAppointmentDetailsByIdAsync(id, userId);

            if (appointment == null)
            {
                return NotFound();
            }

            //var model = new AppointmentInfoViewModel()
            //{
            //    Id = id,
            //    Description = appointment?.Description,
            //    StartDate = appointment?.StartDate.ToString(AppointmentTimeFormat),
            //    EndDate = appointment?.EndDate.ToString(AppointmentTimeFormat),
            //    PatientFirstName = appointment.Patient.FirstName,
            //    PatientLastName = appointment.Patient.LastName
            //};

            return View(appointment);
        }




        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Details", "Practitioner");
            }
            Console.WriteLine($"User ID: {user.Id}");

            var practitioner = await _dbContext.Practitioners
                .FirstOrDefaultAsync(p => p.Id == user.Id);

            if (practitioner == null)
            {
                Console.WriteLine("Practitioner record not found for the current user.");

                ModelState.AddModelError("", "You must be registered as a practitioner to create an appointment.");
                return RedirectToAction("Index");
            }

            var model = new CreateAppointmentViewModel()
            {
                PractitionerId = practitioner.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAppointmentViewModel model)
        {
            try
            {
                bool isAppointmentStartDateValid = DateTime.TryParseExact(model.StartDate, AppointmentTimeFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentStartDate);

                if (!isAppointmentStartDateValid)
                {
                    this.ModelState.AddModelError(nameof(model.StartDate),
                        $"The date should be in the following format: {AppointmentTimeFormat}");
                    return this.View(model);
                }

                bool isAppointmentEndDateValid = DateTime.TryParseExact(model.EndDate, AppointmentTimeFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentEndDate);

                if (!isAppointmentEndDateValid)
                {
                    this.ModelState.AddModelError(nameof(model.EndDate),
                        $"The date should be in the following format: {AppointmentTimeFormat}");
                    return this.View(model);
                }

                if (!ModelState.IsValid)
                {
                    return this.View(model);
                }

                // Lookup practitioner in the database
                var practitioner = await _dbContext.Practitioners.FirstOrDefaultAsync(p => p.Id == model.PractitionerId);
                if (practitioner == null)
                {
                    ModelState.AddModelError("", "The specified practitioner does not exist.");
                    return View(model);
                }


                // Lookup the patient by first name and last name
                var patient = await _dbContext.Patients
                    .FirstOrDefaultAsync(p => p.FirstName == model.PatientFirstName && p.LastName == model.PatientLastName);

                if (patient == null)
                {
                    ModelState.AddModelError("", "The specified patient could not be found.");
                    return View(model);
                }

                Appointment appointment = new Appointment()
                {
                    Id = model.Id,
                    PractitionerId = practitioner.Id,
                    PatientId = patient.Id,
                    Description = model.Description,
                    StartDate = appointmentStartDate,
                    EndDate = appointmentEndDate
                };

                // Add the appointment to the database
                _dbContext.Appointments.Add(appointment);
                var result = await _dbContext.SaveChangesAsync();
                if (result == 0)
                {
                    Console.WriteLine("SaveChangesAsync did not insert any rows.");
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", "An error occurred while creating the appointment.");
                Console.WriteLine("Exception caught: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var appointment = await _dbContext.Appointments
                .Include(appointment => appointment.Patient)
                .FirstOrDefaultAsync(a => a.Id == id && a.PractitionerId == userId);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = new EditAppointmentViewModel()
            {
                Id = appointment.Id
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditAppointmentViewModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


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

        //TODO:
        /*

        // GET: AppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        */

    }
}
