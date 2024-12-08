using Microsoft.AspNetCore.Mvc;

namespace PatientManagementApp.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Errors/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    ViewData["Title"] = "400 - Bad Request";
                    ViewData["Message"] = "The server could not understand the request due to invalid syntax.";
                    return View("BadRequest");
                case 401:
                    ViewData["Title"] = "401 - Unauthorized";
                    ViewData["Message"] = "You are not authorized to access this page.";
                    return View("Unauthorized");
                case 404:
                    ViewData["Title"] = "404 - Not Found";
                    ViewData["Message"] = "The page you are looking for could not be found.";
                    return View("NotFound");
                case 500:
                    ViewData["Title"] = "500 - Internal Server Error";
                    ViewData["Message"] = "An unexpected error occurred on the server.";
                    return View("InternalServerError");
                default:
                    ViewData["Title"] = "Error";
                    ViewData["Message"] = "An unexpected error occurred.";
                    return View("GenericError"); // Generic error view for unspecified cases
            }
        }
    }

}
