using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementApp.Data.Models
{
    public class Diagnosis
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] 
        public string Name { get; set; } = null!;


        [Required] 
        public string Description { get; set; } = null!;

        public virtual ICollection<PatientsDiagnoses> PatientsDiagnoses { get; set; } = new List<PatientsDiagnoses>();

    }
}
