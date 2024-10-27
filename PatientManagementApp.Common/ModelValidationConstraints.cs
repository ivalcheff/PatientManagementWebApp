

namespace PatientManagementApp.Common
{
    public static class ModelValidationConstraints
    {
        public static class Patient
        {
            public const int PatientFirstNameMinLength = 2;
            public const int PatientFirstNameMaxLength = 50;

            public const int PatientLastNameMinLength = 2;
            public const int PatientLastNameMaxLength = 80;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 50;

            public const int PhoneMinLength = 6;
            public const int PhoneMaxLength = 15;



        }


    }
}
