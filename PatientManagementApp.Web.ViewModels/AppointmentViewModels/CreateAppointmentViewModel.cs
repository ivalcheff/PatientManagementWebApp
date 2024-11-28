using PatientManagementApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using PatientManagementApp.Data.Models;
using static PatientManagementApp.Common.ModelValidationConstraints.Appointment;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

namespace PatientManagementApp.Web.ViewModels.AppointmentViewModels
{
    public class CreateAppointmentViewModel : IMapFrom<Appointment>
    {
        public int Id { get; set; }

        [MinLength(AppointmentDescriptionMinLength)]
        [MaxLength(AppointmentDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public string StartDate { get; set; } = null!;
        public string? EndDate { get; set; }


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
