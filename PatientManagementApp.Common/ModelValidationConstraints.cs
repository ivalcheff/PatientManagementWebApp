

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

            public const int PatientImportantInfoMinLength = 5;
            public const int PatientImportantInfoMaxLength = 200;

            public const int ReasonForVisitMinLength = 5;
            public const int ReasonForVisitMaxLength = 200;

            public const int ReferredByMinLength = 2;
            public const int ReferredByMaxLength = 50;

            public const int FeedbackMinLength = 3;
            public const int FeedbackMaxLength = 500;

            public const string DateFormat = "dd.mm.yyyy";
        }


        public static class Appointment
        {
            public const int AppointmentDescriptionMinLength = 2;
            public const int AppointmentDescriptionMaxLength = 200;

        }

        public static class Diagnosis
        {
            public const int DiagnosisNameMinLength = 2;
            public const int DiagnosisNameMaxLength = 100;

            public const int DiagnosisDescriptionMinLength = 10;
            public const int DiagnosisDescriptionMaxLength = 500;

        }

        public static class EmergencyContact
        {
            public const int EmergencyContactNameMinLength = 2;
            public const int EmergencyContactNameMaxLength = 200;
            public const int EmergencyContactRelationshipMinLength = 2;
            public const int EmergencyContactRelationshipMaxLength = 50;
            public const int PhoneMinLength = 6;
            public const int PhoneMaxLength = 15;

        }

        public static class Note
        {
            public const int NoteTextMinLength = 3;
            public const int NoteTextMaxLength = 5000;
        }

        public static class Medication
        {
            public const int MedicationNameMinLength = 2;
            public const int MedicationNameMaxLength = 50;

            public const int MedicationProducerMinLength = 2;
            public const int MedicationProducerMaxLength = 50;

            public const int MedicationDescriptionMinLength = 2;
            public const int MedicationDescriptionMaxLength = 500;
        }

        public static class Practitioner
        {
            public const int PractitionerFirstNameMinLength = 2;
            public const int PractitionerFirstNameMaxLength = 50;

            public const int PractitionerLastNameMinLength = 20;
            public const int PractitionerLastNameMaxLength = 50;

            public const int PhoneMinLength = 6;
            public const int PhoneMaxLength = 15;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 50;

        }

        public static class Specialty
        {
            public const int SpecialtyNameMinLength = 3;
            public const int SpecialtyNameMaxLength = 200;

        }

        public static class FileUpload
        {
            public const int FileNameMinLength = 3;
            public const int FileNameMaxLength = 200;

            public const int ContentTypeMinLength = 3;
            public const int ContentTypeMaxLength = 20;

        }

    }
}
