namespace PatientManagementApp.Web.ViewModels.AppointmentViewModels
{
    public class AppointmentInfoViewModel
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }

        public string PatientFirstName { get; set; } = null!;
        public string PatientLastName { get; set; } = null!;


    }
}
