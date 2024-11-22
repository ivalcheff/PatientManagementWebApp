using System.Globalization;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

namespace PatientManagementApp.Web.Controllers
{
    [Authorize]

    public class AppointmentController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) 
        : BaseController
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        // GET: AppointmentController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
           var user = await _userManager.GetUserAsync(User);
           var userId = user?.Id;

           var appointments = await this._dbContext
               .Appointments
               .Where(a => a.PractitionerId == userId)
               .Select(a => new AppointmentInfoViewModel()
               {
                   Id = a.Id,
                   Description = a.Description,
                   StartDateTime = a.StartDate.ToString(AppointmentTimeFormat),
                   EndDateTime = a.EndDate.ToString(AppointmentTimeFormat),
                   PatientFirstName = a.Patient.FirstName,
                   PatientLastName = a.Patient.LastName
               })
               .AsNoTracking()
               .ToListAsync();


           return View(appointments);
        }

        // GET: AppointmentController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
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

            var model = new AppointmentInfoViewModel()
            {
                Id = id,
                Description = appointment?.Description,
                StartDateTime = appointment?.StartDate.ToString(AppointmentTimeFormat),
                EndDateTime = appointment?.EndDate.ToString(AppointmentTimeFormat),
                PatientFirstName = appointment.Patient.FirstName,
                PatientLastName = appointment.Patient.LastName
            };

            return View(model);
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
                bool isAppointmentStartDateValid = DateTime.TryParseExact(model.StartDateTime, AppointmentTimeFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentStartDate);

                if (!isAppointmentStartDateValid)
                {
                    this.ModelState.AddModelError(nameof(model.StartDateTime),
                        $"The date should be in the following format: {AppointmentTimeFormat}");
                    return this.View(model);
                }

                bool isAppointmentEndDateValid = DateTime.TryParseExact(model.EndDateTime, AppointmentTimeFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentEndDate);

                if (!isAppointmentEndDateValid)
                {
                    this.ModelState.AddModelError(nameof(model.EndDateTime),
                        $"The date should be in the following format: {AppointmentTimeFormat}");
                    return this.View(model);
                }

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Model state is not valid");
                    return this.View(model);
                }

                Console.WriteLine("Model state is valid");


                // Lookup practitioner in the database
                var practitioner = await _dbContext.Practitioners.FirstOrDefaultAsync(p => p.Id == model.PractitionerId);
                if (practitioner == null)
                {
                    ModelState.AddModelError("", "The specified practitioner does not exist.");
                    Console.WriteLine("Practitioner not found.");

                    return View(model);
                }

                Console.WriteLine("Practitioner exists");

                // Lookup the patient by first name and last name
                var patient = await _dbContext.Patients
                    .FirstOrDefaultAsync(p => p.FirstName == model.PatientFirstName && p.LastName == model.PatientLastName);

                if (patient == null)
                {
                    ModelState.AddModelError("", "The specified patient could not be found.");
                    Console.WriteLine("Patient not found.");
                    return View(model);
                }

                Console.WriteLine("patient exists");

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

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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




        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var appointments = await _dbContext.Appointments
                .Where(a => a.PractitionerId == userId)
                .Include(a => a.Patient)
                .ToListAsync();

            var events = appointments.Select(a => new
            {
                title = $"{a.Patient.FirstName} {a.Patient.LastName} - {a.Description}",
                start = a.StartDate.ToString(AppointmentTimeFormat),
                end = a.EndDate.ToString(AppointmentTimeFormat),
                id = a.Id
            });

            return new JsonResult(events);
        }

    }
}
