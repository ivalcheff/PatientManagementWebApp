
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static PatientManagementApp.Common.ModelValidationConstraints.Appointment;

namespace PatientManagementApp.Data.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(AppointmentDescriptionMaxLength)]
        [Comment("Appointment description")]
        public string? Description { get; set; }

        [Comment("Appointment starting time")]
        public DateTime StartDate { get; set; }

        [Comment("Appointment end time")]
        public DateTime EndDate { get; set; }

        public Guid PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; } = null!;

        public Guid PractitionerId { get; set; }
        [ForeignKey(nameof(PractitionerId))]
        public virtual Practitioner Practitioner { get; set; } = null!;

        [Comment("for soft-delete purposes")]
        public bool IsDeleted { get; set; }

    }
}
