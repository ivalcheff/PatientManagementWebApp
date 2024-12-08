
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration
{
    public class PractitionerConfiguration : IEntityTypeConfiguration<Practitioner>
    {
        public void Configure(EntityTypeBuilder<Practitioner> builder)
        {
            builder
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Practitioner>(p => p.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
