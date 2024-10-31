
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Diagnosis;


namespace PatientManagementApp.Data.Models
{
    public class Diagnosis
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] 
        [MaxLength(DiagnosisNameMaxLength)]
        [Comment("The patient's diagnosis")]
        public string Name { get; set; } = null!;


        [Required] 
        [MaxLength(DiagnosisDescriptionMaxLength)]
        [Comment("Description of the diagnosis")]
        public string Description { get; set; } = null!;

        public virtual ICollection<PatientsDiagnoses> PatientsDiagnoses { get; set; } = new HashSet<PatientsDiagnoses>();

    }
}
