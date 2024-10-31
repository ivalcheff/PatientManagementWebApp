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
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasData(SeedSpecialties());
        }

        private List<Specialty> SeedSpecialties()
        {
            List<Specialty> specialties = new List<Specialty>()
            {
                new Specialty()
                {
                    Name = "Psychiatrist"
                },

                new Specialty()
                {
                    Name = "Psychologist"
                },
                new Specialty()
                {
                    Name = "Hypnotherapist"
                },
                new Specialty()
                {
                    Name = "Speech therapist"
                },
                new Specialty()
                {
                    Name = "Clinical psychologist"
                },
                new Specialty()
                {
                    Name = "Group therapist"
                },
                new Specialty()
                {
                    Name = "Family therapist"
                },
                new Specialty()
                {
                    Name = "Psychotherapist"
                }

            };
                return specialties;
        }

    }
}
