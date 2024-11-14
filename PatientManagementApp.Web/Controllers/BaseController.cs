using Microsoft.AspNetCore.Mvc;

namespace PatientManagementApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsGuidValid(string id, ref Guid patientGuid)
        {
            //non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            //invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out patientGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
