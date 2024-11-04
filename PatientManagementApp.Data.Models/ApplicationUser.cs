using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PatientManagementApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        public Guid PractitionerId { get; set; }
        public virtual Practitioner Practitioner { get; set; } = null!;



    }
}
