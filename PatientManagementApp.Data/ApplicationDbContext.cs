using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            builder.Entity<IdentityUserRole<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
        }


        public async Task SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed practitioners as users
            if (!await userManager.Users.AnyAsync())
            {
                var practitioners = new[]
                {
                    new { Email = "practitioner1@example.com", FirstName = "Ivan", LastName = "Ivanov" },
                    new { Email = "practitioner2@example.com", FirstName = "Dimitar", LastName = "Dimitrov" },
                };

                foreach (var practitionerData in practitioners)
                {
                    // Check if the practitioner user already exists
                    var user = await userManager.FindByEmailAsync(practitionerData.Email);
                    if (user == null)
                    {
                        // Create ApplicationUser
                        user = new ApplicationUser { UserName = practitionerData.Email, Email = practitionerData.Email };
                        var result = await userManager.CreateAsync(user, "Password@123"); // Set a strong password

                        if (result.Succeeded)
                        {
                            // Assign the "User" role
                            await userManager.AddToRoleAsync(user, "User");

                            // Create and link Practitioner entity
                            var practitioner = new Practitioner
                            {
                                Id = Guid.NewGuid(),
                                FirstName = practitionerData.FirstName,
                                LastName = practitionerData.LastName,
                                UserId = user.Id // Link to ApplicationUser ID
                            };

                            Practitioners.Add(practitioner);
                        }
                    }
                }
            }

            await SaveChangesAsync();
        }

    }
}
