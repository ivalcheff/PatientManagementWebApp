using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration
{
    public class PractitionersSpecialtiesConfiguration :IEntityTypeConfiguration<PractitionersSpecialties>
    {
        public void Configure(EntityTypeBuilder<PractitionersSpecialties> builder)
        {
            builder.HasKey(ps => new { ps.PractitionerId, ps.SpecialtyId });

            builder.HasOne(ps => ps.Practitioner)
                .WithMany(p => p.PractitionersSpecialties)
                .HasForeignKey(ps => ps.PractitionerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ps => ps.Specialty)
                .WithMany(s => s.PractitionersSpecialties)
                .HasForeignKey(ps => ps.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
