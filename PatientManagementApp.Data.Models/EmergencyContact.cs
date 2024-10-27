
using System.ComponentModel.DataAnnotations;
using static PatientManagementApp.Common.ModelValidationConstraints;

namespace PatientManagementApp.Data.Models
{
    public class EmergencyContact
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? Relationship { get; set; }


    }
}
