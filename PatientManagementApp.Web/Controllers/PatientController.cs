using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using PatientManagementApp.Common;
using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]
    public class PatientController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        : BaseController
    {

        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            IEnumerable<Patient> patients = await this._dbContext
                .Patients
                .ToListAsync();

            return View(patients);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            bool isTreatmentStartDateValid = DateTime.TryParseExact(model.TreatmentStartDate,DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime treatmentStartDate);

            if (!isTreatmentStartDateValid)
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate),
                    $"The date should be in the following format: {DateFormat}");
                return this.View(model);
            }

            bool isDateOfBirthValid = DateTime.TryParseExact(model.DateOfBirth, DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth);

            if (!isDateOfBirthValid)
            {
                this.ModelState.AddModelError(nameof(model.DateOfBirth),
                    $"The date should be in the following format:{DateFormat}");
                return this.View(model);
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            Patient patient = new Patient()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                BirthDate = dateOfBirth,
                Age = model.Age,
                //TODO add gender as enum
                //TODO add diagnoses
                TreatmentStartDate = dateOfBirth,
                ReasonForVisit = model.ReasonForVisit,
                ReferredBy = model.ReferredBy,
                ImportantInfo = model.ImportantInfo,
                EmergencyContact = new EmergencyContact()
                {
                    Name = model.EmergencyContactName,
                    PhoneNumber = model.EmergencyContactPhone,
                    Relationship = model.EmergencyContactRelationship
                },
                Status = Enums.PatientStatus.Active,
                IsActive = true,
                PractitionerId = (Guid)userId,

            };

            await this._dbContext.Patients.AddAsync(patient);
            await this._dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            //check if id is valid

            Guid patientGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref patientGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var patient = await _dbContext.Patients
                .Where(p => p.Id == patientGuid)
                .Where(p => p.IsActive == true)
                .AsNoTracking()
                .Select(p => new PatientDetailsViewModel()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Gender = p.Gender, //doesn't work great; displays null
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    DateOfBirth = p.BirthDate.ToString(DateFormat),
                    TreatmentStartDate = p.TreatmentStartDate.ToString(DateFormat),
                    TreatmentEndDate = p.TreatmentEndDate.ToString(DateFormat),
                    ReasonForVisit = p.ReasonForVisit,
                    ReferredBy = p.ReferredBy,
                    ImportantInfo = p.ImportantInfo,
                    Status = p.Status,
                    Feedback = p.Feedback,
                    EmergencyContactName = p.EmergencyContact.Name,
                    EmergencyContactPhoneNumber = p.EmergencyContact.PhoneNumber,
                    EmergencyContactRelationship = p.EmergencyContact.Relationship
                })
                .FirstOrDefaultAsync();


            return this.View(patient);
        }


        //EDIT PATIENT
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            //check if id is valid
            Guid patientGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref patientGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var patient = await _dbContext.Patients
                .Where(p => p.Id == patientGuid)
                .Where(p => p.IsActive == true)
                .AsNoTracking()
                .Select(p => new PatientDetailsViewModel()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Gender = p.Gender,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    DateOfBirth = p.BirthDate.ToString(DateFormat),
                    TreatmentStartDate = p.TreatmentStartDate.ToString(DateFormat),
                    TreatmentEndDate = p.TreatmentEndDate.ToString(DateFormat),
                    ReasonForVisit = p.ReasonForVisit,
                    ReferredBy = p.ReferredBy,
                    ImportantInfo = p.ImportantInfo,
                    Status = p.Status,
                    Feedback = p.Feedback,
                    EmergencyContactName = p.EmergencyContact.Name,
                    EmergencyContactPhoneNumber = p.EmergencyContact.PhoneNumber,
                    EmergencyContactRelationship = p.EmergencyContact.Relationship
                })
                .FirstOrDefaultAsync();


            return View(patient);
        }
    }
}
