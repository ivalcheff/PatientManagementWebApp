

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data.Configuration
{
    public class PatientsMedicationsConfiguration : IEntityTypeConfiguration<PatientsMedications>
    {
        public void Configure(EntityTypeBuilder<PatientsMedications> builder)
        {
            throw new NotImplementedException();
        }
    }
}
