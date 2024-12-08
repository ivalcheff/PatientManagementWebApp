using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.PatientViewModels;

using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using static PatientManagementApp.Common.Enums;


namespace PatientManagementApp.Web.Controllers
{
    [Authorize]
    public class PatientController(IPatientService patientService
                                ,UserManager<ApplicationUser> userManager) 
        : BaseController
    {
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

            PatientDetailsViewModel? patient = await this._patientService
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
            if (id == Guid.Empty)
            {
                return this.RedirectToAction(nameof(Index));
            }

            EditPatientViewModel? model = await this._patientService
                .GetEditPatientModelByIdAsync(id);

            if (model == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            model.GenderOptions = GetGenderOptions();
            model.PatientStatusOptions = GetStatusOptions();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPatientViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return View(model);
            }

            bool isUpdated = await this._patientService
                .EditPatientAsync(model);

            if (!isUpdated)
            {
                this.ModelState.AddModelError("", $"A problem occured when trying to update the patient");
                model.GenderOptions = GetGenderOptions();
                model.PatientStatusOptions = GetStatusOptions();
                return this.View(model);
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
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
