using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using PatientManagementApp.Common;
using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.Enums;


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]
    public class PatientController(ApplicationDbContext dbContext
        ,IPatientService patientService
        ,UserManager<ApplicationUser> userManager)
        : BaseController
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IPatientService _patientService = patientService;


        //INDEX

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            IEnumerable<PatientDetailsViewModel> patients = 
                await this._patientService
                .IndexAllFromCurrentUser(userId);

            return View(patients);
        }


        //DETAILS

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)  
        {
            if (id == Guid.Empty)
            {
                return this.RedirectToAction(nameof(Index));
            }
            var patient = await this._patientService
                .GetPatientDetailsByIdAsync(id);

            if (patient == null)
            {
                ModelState.AddModelError("", "A patient with this ID does not exist.");
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(patient);
        }

        //CREATE NEW PATIENT

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreatePatientViewModel();
            model.GenderOptions = GetGenderOptions();
            model.PatientStatusOptions = GetStatusOptions();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            if (!ValidateDate(model.TreatmentStartDate, DateFormat, out DateTime treatmentStartDate,
                    out string validationMessage))
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate), validationMessage);
                return this.View(model);

            }

            if (!ValidateDate(model.DateOfBirth, DateFormat, out DateTime dateOfBirth, out string birthdayValidationMessage))
            {
                this.ModelState.AddModelError(nameof(model.DateOfBirth), birthdayValidationMessage);
                return this.View(model);

            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            model.GenderOptions = GetGenderOptions();
            model.PatientStatusOptions = GetStatusOptions();

            await this._patientService.AddNewPatientAsync(model, dateOfBirth, treatmentStartDate, userId);

            return this.RedirectToAction(nameof(Index));
        }





        //EDIT PATIENT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var patient = await _dbContext.Patients
                .Where(p => p.Id == id)
                .Where(p => p.IsActive == true)
                .Include(p => p.EmergencyContact)
                .AsNoTracking()
                .Select(p => new EditPatientViewModel()
                {
                    Id = p.Id,
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

            patient.GenderOptions = GetGenderOptions();
            patient.PatientStatusOptions = GetStatusOptions();

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPatientViewModel model, Guid id)
        {
            if (ModelState.IsValid == false)
            {
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return View(model);

            }

            if (DateTime.TryParseExact(model.TreatmentStartDate, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var treatmentStartDate) == false)
            {
                ModelState.AddModelError(nameof(model.TreatmentStartDate), "Invalid date format");
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return View(model);
            }

            if (DateTime.TryParseExact(model.TreatmentEndDate, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var treatmentEndDate) == false)
            {
                ModelState.AddModelError(nameof(model.TreatmentEndDate), "Invalid date format");
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return View(model);
            }

            if (DateTime.TryParseExact(model.DateOfBirth, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var birthDate) == false)
            {
                ModelState.AddModelError(nameof(model.DateOfBirth), "Invalid date format");
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return View(model);
            }

            Patient? patient = await _dbContext
                .Patients
                .Include(p => p.EmergencyContact)
                .FirstOrDefaultAsync(p => p.Id == id);

            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.Email = model.Email;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Age = model.Age;
            patient.Gender = model.Gender;
            patient.BirthDate = birthDate;
            patient.TreatmentStartDate = treatmentStartDate;
            patient.TreatmentEndDate = treatmentEndDate;
            patient.ReasonForVisit = model.ReasonForVisit;
            patient.ReferredBy = model.ReferredBy;
            patient.ImportantInfo = model.ImportantInfo;
            patient.Status = model.Status;
            patient.Feedback = model.Feedback;
            patient.EmergencyContact.Name = model.EmergencyContactName;
            patient.EmergencyContact.PhoneNumber = model.EmergencyContactPhoneNumber;
            patient.EmergencyContact.Relationship = model.EmergencyContactRelationship;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        //GET ENUMS

        private List<SelectListItem> GetGenderOptions()
        {
            var gendersList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                })
                .ToList();

            return gendersList;
        }

        private List<SelectListItem> GetStatusOptions()
        {
            var patientStatusList = Enum.GetValues(typeof(PatientStatus))
                .Cast<PatientStatus>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                })
                .ToList();

            return patientStatusList;
        }


    }
}
