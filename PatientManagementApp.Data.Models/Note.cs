using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementApp.Data.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }

        [Required] [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; } = null!;

        [Required] [MaxLength] 
        public string NoteText { get; set; } = null!;
    }
}
