using Microsoft.AspNetCore.Mvc;

namespace PatientManagementApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsGuidValid(string id, ref Guid cinemaGuid)
        {
            //non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            //invalid parameter int he URL
            bool isGuidValid = Guid.TryParse(id, out cinemaGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
