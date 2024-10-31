

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Common;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .HasOne(p => p.Practitioner)
                .WithMany(p => p.Patients)
                .HasForeignKey(p => p.PractitionerId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasData(SeedPatients());
        }

        private List<Patient> SeedPatients()
        {
            List<Patient> patients = new List<Patient>()
            {
                new Patient()
                {
                    FirstName = "Петър",
                    LastName = "Петров",
                    BirthDate = new DateTime(1994, 05, 01),
                    PhoneNumber = "",
                    Email = "",
                    Age = 30,
                    Gender = Enums.Gender.Male,
                    Status = Enums.PatientStatus.Active,
                    TreatmentStartDate = new DateTime(2024, 03, 30),


                }

            };

            return patients;
        }
    }
}
