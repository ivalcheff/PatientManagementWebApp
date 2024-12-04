
using System.Globalization;


namespace PatientManagementApp.Services.Data
{
    public class BaseService 
    {
       protected bool ValidateDate(string? dateString, string dateFormat, out DateTime parsedDate)
       {
           // If the date string is null or empty, it means the date is optional, so it's valid.
           if (string.IsNullOrWhiteSpace(dateString))
           {
               parsedDate = default;
               return true;
           }

            bool isValid = DateTime.TryParseExact(
                dateString,
                dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);

            Console.WriteLine($"Validating Date: Input: {dateString}, Expected Format: {dateFormat}, Valid: {isValid}");

            return isValid;
       }
        
    }
}
