

using System.ComponentModel.DataAnnotations;
using static PatientManagementApp.Common.ModelValidationConstraints.Appointment;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

namespace PatientManagementApp.Web.ViewModels.AppointmentViewModels
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }

        [MinLength(AppointmentDescriptionMinLength)]
        [MaxLength(AppointmentDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]

        public string StartDateTime { get; set; } = null!;
        public string? EndDateTime { get; set; }


        public Guid PractitionerId { get; set; }
        public Guid PatientId { get; set; }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string PatientFirstName { get; set; } = null!;

        [Required]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string PatientLastName { get; set; } = null!;
    }
}
