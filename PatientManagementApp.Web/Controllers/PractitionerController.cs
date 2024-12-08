using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;
using PatientManagementApp.Services.Data.Interfaces;

namespace PatientManagementApp.Web.Controllers
{
    [Authorize]

    public class PractitionerController(IPractitionerService practitionerService
                                        ,UserManager<ApplicationUser> userManager) 
        : BaseController
    {

        private readonly IPractitionerService _practitionerService = practitionerService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        //INDEX

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user!.Id;

            // Check if the user has the Admin role
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            IEnumerable<PractitionerDetailsViewModel> practitioners;

            if (isAdmin)
            {
                practitioners = await this._practitionerService.IndexAllPractitionersAsync();
                ViewBag.Title = "All Patients";
            }
            else
            {
                return NotFound();
            }

            return View(practitioners);
        }

        //DETAILS

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            var user = await _userManager.GetUserAsync(User);
            id ??= user?.Id;

            if (id == null)
            {
                return Unauthorized();
            }

            PractitionerDetailsViewModel practitionerDetails;
            try
            {
                practitionerDetails = await this._practitionerService.GetPractitionerDetailsByIdAsync(id.Value);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return View(practitionerDetails);
        }


        //EDIT

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var model = await _practitionerService.GetPractitionerEditModelByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PractitionerEditViewModel model, Guid id)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUpdated = await _practitionerService.EditPractitionerAsync(model);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details));

        }


        //SOFT-DELETE

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(Guid id)
        {
            DeletePractitionerViewModel? model = await this._practitionerService
                .GetPractitionerDeleteModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDeleteConfirmed(DeletePractitionerViewModel model)
        {
           
            bool isDeleted = await this._practitionerService.SoftDeletePractitionerAsync(model.Id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Unexpected error occured. Could not delete practitioner.";
                return this.RedirectToAction(nameof(Delete), new { id = model.Id });
            }

            TempData["SuccessMessage"] = "Practitioner and related records have been successfully marked as inactive.";
            return this.RedirectToAction(nameof(Index));

        }

    }
}
