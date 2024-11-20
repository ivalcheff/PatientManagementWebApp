﻿

using System.ComponentModel.DataAnnotations;

using PatientManagementApp.Data.Models;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


namespace PatientManagementApp.Web.ViewModels.PractitionerViewModels
{
    public class PractitionerEditViewModel
    {
        public Guid Id { get; set; }

        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string? LastName { get; set; }

        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsAvailableOnline { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

        public List<AppointmentInfoViewModel> Appointments { get; set; } = new List<AppointmentInfoViewModel>();

        
    }
}
