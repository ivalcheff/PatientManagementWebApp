using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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


        protected bool ValidateDate(string dateString, string dateFormat, out DateTime parsedDate, out string validationMessage)
        {
            // Attempt to parse the provided date string with the specified format
            bool isValid = DateTime.TryParseExact(
                dateString,
                dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);

            validationMessage = isValid ? string.Empty : $"The date should be in the following format: {dateFormat}";

            return isValid;
        }
    }
}
