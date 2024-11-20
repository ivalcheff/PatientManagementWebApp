using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;


namespace PatientManagementApp.Data.Configuration
{
    public class PatientsMedicationsConfiguration : IEntityTypeConfiguration<PatientsMedications>
    {
        public void Configure(EntityTypeBuilder<PatientsMedications> builder)
        {
            builder.HasKey(pm => new { pm.PatientId, pm.MedicationId });

            builder.HasOne(pm => pm.Patient)
                .WithMany(p => p.PatientsMedications)
                .HasForeignKey(pm => pm.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pm => pm.Medication)
                .WithMany(m => m.PatientsMedications)
                .HasForeignKey(m => m.MedicationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
