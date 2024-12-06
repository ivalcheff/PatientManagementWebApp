
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration 
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
