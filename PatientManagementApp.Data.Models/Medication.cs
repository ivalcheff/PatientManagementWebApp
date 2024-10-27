
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Medication;


namespace PatientManagementApp.Data.Models
{
    public class Medication
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(MedicationNameMaxLength)]
        [Comment("The name of the medication")]
        public string Name { get; set; } = null!;

        [MaxLength(MedicationDescriptionMaxLength)]
        [Comment("The description of the medication")]
        public string? Description { get; set; }

        [Required]
        [MaxLength(MedicationProducerMaxLength)]
        [Comment("The medication's producer")]
        public string Producer { get; set; } = null!;

        [Required]
        [Comment("The dosage per day")]
        public decimal Dosage { get; set; }
    }
}
