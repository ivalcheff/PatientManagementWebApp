using PatientManagementApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementApp.Services.Data
{
    public class BaseService 
    {
        
       protected bool ValidateDate(string dateString, string dateFormat, out DateTime parsedDate)
        {
            bool isValid = DateTime.TryParseExact(
                dateString,
                dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);

            return isValid;
        }
        
    }
}
