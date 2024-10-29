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
        public Guid? PractitionerId { get; set; }

        [ForeignKey(nameof(PractitionerId))]
        public virtual Practitioner? Practitioner { get; set; }
        

        public Guid? PatientId { get; set; }
        
        [ForeignKey(nameof(PatientId))]
        public virtual Patient? Patient { get; set; }

        public string UserType { get; set; } 


    }
}
