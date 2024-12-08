using AutoMapper;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Services.Mapping;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Web.ViewModels.AppointmentViewModels
{
    public class AppointmentInfoViewModel:IMapFrom<Appointment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public string PatientFirstName { get; set; } = null!;
        public string PatientLastName { get; set; } = null!;


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, AppointmentInfoViewModel>()
                .ForMember(d => d.StartDate,
                    x => x.MapFrom(s => s.StartDate.ToString(AppointmentTimeFormat)))
                .ForMember(d => d.EndDate,
                    x => x.MapFrom(s => s.EndDate.ToString(AppointmentTimeFormat)));
        }
    }
}
