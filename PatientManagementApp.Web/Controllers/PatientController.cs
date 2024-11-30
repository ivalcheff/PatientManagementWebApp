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
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Patient;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.Enums;


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

            var patients = await this._dbContext
                .Patients
                .Where(p => p.IsActive)
                .Where(p => p.PractitionerId == userId)
                .Select(p => new PatientDetailsViewModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    Age = p.Age,
                    Status = p.Status,
                    PhoneNumber = p.PhoneNumber
                })
                .AsNoTracking()
                .ToListAsync();

            return View(patients);
        }


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
            var userId = user?.Id;

            if (!ValidateDate(model.TreatmentStartDate, DateFormat, out DateTime treatmentStartData,
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

            Patient patient = new Patient()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                BirthDate = dateOfBirth,
                Age = model.Age,
                Gender = model.Gender,
                Diagnosis = model.Diagnosis,
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
                Status = model.Status,
                IsActive = true,
                PractitionerId = (Guid)userId,

            };

            await this._dbContext.Patients.AddAsync(patient);
            await this._dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Details(Guid id)  //does not work
        {
            ////check if id is valid
            //Guid patientGuid = Guid.Empty;
            //bool isIdValid = this.IsGuidValid(id, ref patientGuid);

            //if (!isIdValid)
            //{
            //    Console.WriteLine("Invalid id");
            //    return this.RedirectToAction(nameof(Index));
            //}

            Patient? patient = await this._dbContext
                .Patients
                .Include(p => p.EmergencyContact)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                //checks if such a patient exists in db
                return this.RedirectToAction(nameof(Index));
            }

            var model = new PatientDetailsViewModel()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Gender = patient.Gender, //doesn't work great; displays null
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.BirthDate.ToString(DateFormat) ?? "N/A",
                TreatmentStartDate = patient.TreatmentStartDate.ToString(DateFormat),
                TreatmentEndDate = patient.TreatmentEndDate.ToString(DateFormat),
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


        //EDIT PATIENT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ////check if id is valid
            //Guid patientGuid = Guid.Empty;
            //bool isGuidValid = this.IsGuidValid(id, ref patientGuid);

            //if (!isGuidValid)
            //{
            //    return this.RedirectToAction(nameof(Index));
            //}

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
