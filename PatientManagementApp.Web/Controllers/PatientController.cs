using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.PatientViewModels;

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

            // Check if the user has the Admin role
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            IEnumerable<PatientDetailsViewModel> patients;

            if (isAdmin)
            {
                patients = await this._patientService.IndexAllPatientsAsync();
                ViewBag.Title = "All Patients"; 
            }
            else
            {
                // Get only the patients of the current user
                patients = await this._patientService.IndexAllFromCurrentUser(userId);
                ViewBag.Title = "My Patients"; 
            }

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

            if (!ModelState.IsValid)
            {
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
            }

            bool result = await this._patientService.AddNewPatientAsync(model, userId);

            if (!result)
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate), $"The date should be in the following format: {DateFormatString}");
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
            }

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
                    DateOfBirth = p.BirthDate.ToString(DateFormatString),
                    TreatmentStartDate = p.TreatmentStartDate.ToString(DateFormatString),
                    TreatmentEndDate = p.TreatmentEndDate.ToString(DateFormatString),
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

            if (!ValidateDate(model.TreatmentStartDate, DateFormatString, out DateTime treatmentStartDate,
                    out string validationMessage))
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate), validationMessage);
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
            }

            if (!ValidateDate(model.TreatmentEndDate, DateFormatString, out DateTime treatmentEndDate,
                    out string endDateValidationMessage))
            {
                this.ModelState.AddModelError(nameof(model.TreatmentStartDate), endDateValidationMessage);
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
            }

            if (!ValidateDate(model.DateOfBirth, DateFormatString, out DateTime dateOfBirth, out string birthdayValidationMessage))
            {
                this.ModelState.AddModelError(nameof(model.DateOfBirth), birthdayValidationMessage);
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
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
            patient.BirthDate = dateOfBirth;
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
