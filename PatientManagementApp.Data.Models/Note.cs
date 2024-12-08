
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Note;


namespace PatientManagementApp.Data.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid PatientId { get; set; }

        [Required] 
        [ForeignKey(nameof(PatientId))]
        [Comment("The patient the note is for")]
        public virtual Patient Patient { get; set; } = null!;

        [Required] 
        [MaxLength(NoteTextMaxLength)]
        [Comment("The content of the note")]
        public string NoteText { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
