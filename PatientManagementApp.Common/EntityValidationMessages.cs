
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
namespace PatientManagementApp.Common
{
    public static class EntityValidationMessages
    {
       
        public const string FirstNameIsRequired = "First name is required";
        public const string LastNameIsRequired = "Last name is required";
        public const string PhoneIsRequired = "Phone number is required";
        public const string DateFormatIsIncorrect = $"The date should be in the following format: {DateFormatString}";

    }
}
