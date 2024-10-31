

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration
{
    public class PatientsDiagnosesConfiguration : IEntityTypeConfiguration<PatientsDiagnoses>
    {
        public void Configure(EntityTypeBuilder<PatientsDiagnoses> builder)
        {
           
                builder.HasKey(pd => new { pd.PatientId, pd.DiagnosisId });

                builder.HasOne(pd => pd.Patient)
                    .WithMany(p => p.PatientsDiagnoses)
                    .HasForeignKey(pm => pm.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(pd => pd.Diagnosis)
                    .WithMany(d => d.PatientsDiagnoses)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
