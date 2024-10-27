using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementApp.Data.Models
{
    public class Specialty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public ICollection<PractitionersSpecialties> PractitionersSpecialties { get; set; } = new List<PractitionersSpecialties>();

    }
}
