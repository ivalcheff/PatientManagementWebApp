
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.EmergencyContact;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Data.Models
{
    public class EmergencyContact
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(EmergencyContactNameMaxLength)]
        [Comment("The full name of the patient's emergency contact")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("The emergency contact's phone number")]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(EmergencyContactRelationshipMaxLength)]
        [Comment("The emergency contact's relationship to the patient")]
        public string? Relationship { get; set; }


    }
}
