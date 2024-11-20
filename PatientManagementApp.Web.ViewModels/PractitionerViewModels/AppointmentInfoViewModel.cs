

namespace PatientManagementApp.Web.ViewModels.PractitionerViewModels
{
    public class AppointmentInfoViewModel
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public string PatientFirstName { get; set; } = null!;
        public string PatientLastName { get; set;} = null!;


    }
}
