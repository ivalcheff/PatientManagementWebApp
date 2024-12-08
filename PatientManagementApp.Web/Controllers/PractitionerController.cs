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
    }
}
