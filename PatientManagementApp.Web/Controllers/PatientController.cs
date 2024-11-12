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


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]
    public class PatientController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : BaseController
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
            
            bool isTreatmentStartDateValid = DateTime.TryParseExact(model.TreatmentStartDate, ModelValidationConstraints.Global.DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime treatmentStartDate);

            if (!isTreatmentStartDateValid)
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate), $"The date should be in the following format: {ModelValidationConstraints.Global.DateFormat}");
                return this.View(model);
            }

            bool isDateOfBirthValid = DateTime.TryParseExact(model.DateOfBirth, ModelValidationConstraints.Global.DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth);

            if (!isDateOfBirthValid)
            {
                this.ModelState.AddModelError(nameof(model.DateOfBirth), $"The date should be in the following format:{ModelValidationConstraints.Global.DateFormat}");
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

            //check if such patient exists
            Patient? patient = await this._dbContext
                .Patients
                .FirstOrDefaultAsync(p => p.Id == patientGuid);

            if (patient == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            PatientDetailsViewModel model = new PatientDetailsViewModel()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.BirthDate.ToString(ModelValidationConstraints.Global.DateFormat),
                TreatmentStartDate = patient.TreatmentStartDate.ToString(ModelValidationConstraints.Global.DateFormat),
                TreatmentEndDate = patient.TreatmentEndDate.ToString(ModelValidationConstraints.Global.DateFormat),
                ReasonForVisit = patient.ReasonForVisit,
                ReferredBy = patient.ReferredBy,
                ImportantInfo = patient.ImportantInfo,
                Status = patient.Status,
                Feedback = patient.Feedback,
                EmergencyContactName = patient.EmergencyContact.Name,
                EmergencyContactPhoneNumber = patient.EmergencyContact.PhoneNumber,
                EmergencyContactRelationship = patient.EmergencyContact.Relationship

            };

            return this.View(model);
        }

    }
}
