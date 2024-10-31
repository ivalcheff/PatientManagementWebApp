using System.Reflection;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Diagnosis> Diagnoses { get; set; } = null!; 
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;
        public virtual DbSet<Practitioner> Practitioners { get; set; } = null!;
        public virtual DbSet<FileUpload> Files { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Medication> Medications { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<PatientsDiagnoses> PatientsDiagnoses { get; set; } = null!;
        public virtual DbSet<PatientsMedications> PatientsMedications { get; set; } = null!;
        public virtual DbSet<PractitionersSpecialties> PractitionersSpecialties { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
