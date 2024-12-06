

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;
using static PatientManagementApp.Common.ModelValidationConstraints.Appointment;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;

namespace PatientManagementApp.Web.ViewModels.AppointmentViewModels
{
    public class EditAppointmentViewModel : IMapTo<Appointment>,IMapFrom<Appointment>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, EditAppointmentViewModel>()
                .ForMember(d => d.StartDate,
                    x => x.MapFrom(s => s.StartDate.ToString(AppointmentTimeFormat)))
                .ForMember(d => d.EndDate,
                    x => x.MapFrom(s => s.EndDate.ToString(AppointmentTimeFormat)));

            configuration.CreateMap<EditAppointmentViewModel, Appointment>()
                .ForMember(d => d.StartDate, x => x.Ignore())
                .ForMember(d => d.EndDate, x => x.Ignore())
                .ForMember(d => d.PractitionerId, x => x.Ignore())
                .ForMember(d => d.PatientId, x => x.Ignore()); ;

        }
    }
}
