using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PatientManagementApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public virtual Practitioner Practitioner { get; set; } = null!;

        public bool IsDeleted { get; set; }

        //TODO add a Patient User:
        //public Guid PatientId { get; set; }
        //public virtual Patient? Patient { get; set; }
        //set the Practitioner to a nullable variable

    }
}
