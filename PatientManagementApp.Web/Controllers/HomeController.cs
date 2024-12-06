using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.ViewModels;

namespace PatientManagementApp.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger) 
        : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

       
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
