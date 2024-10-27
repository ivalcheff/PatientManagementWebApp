
using System.ComponentModel.DataAnnotations;
using static PatientManagementApp.Common.ModelValidationConstraints;


namespace PatientManagementApp.Data.Models
{
    public class Practitioner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool IsAvailableOnline { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<PractitionersSpecialties> PractitionersSpecialties { get; set; } = new List<PractitionersSpecialties>();
    }
}
