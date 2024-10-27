
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace PatientManagementApp.Data.Models
{
    [PrimaryKey(nameof(PatientId), nameof(DiagnosisId))]
    public class PatientsDiagnoses
    {

        public Guid PatientId { get; set; }
        [ForeignKey(nameof(PatientId))] 
        public Patient Patient { get; set; } = null!;

        public Guid DiagnosisId { get; set; }
        [ForeignKey(nameof(DiagnosisId))] 
        public Diagnosis Diagnosis { get; set; } = null!;
    }

}
