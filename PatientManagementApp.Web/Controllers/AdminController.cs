using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApp.Services.Data.Interfaces;

namespace PatientManagementApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var model = await _adminService.GetDeletedRecordsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RestorePatient(Guid id)
        {
            var success = await _adminService.RestorePatientAsync(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Could not restore the patient.";
            }
            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        public async Task<IActionResult> RestorePractitioner(Guid id)
        {
            var success = await _adminService.RestorePractitionerAsync(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Could not restore the practitioner.";
            }
            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        public async Task<IActionResult> RestoreAppointment(int id)
        {
            var success = await _adminService.RestoreAppointmentAsync(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Could not restore the appointment.";
            }
            return RedirectToAction(nameof(Manage));
        }
    }

}
