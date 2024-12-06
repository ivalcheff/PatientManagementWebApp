

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder
                .Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

        }

       
    }
}
