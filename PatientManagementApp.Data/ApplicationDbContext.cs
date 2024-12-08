using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;
        public virtual DbSet<Practitioner> Practitioners { get; set; } = null!;
        public virtual DbSet<FileUpload> Files { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Medication> Medications { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<PatientsMedications> PatientsMedications { get; set; } = null!;
        public virtual DbSet<PractitionersSpecialties> PractitionersSpecialties { get; set; } = null!;

        public async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            //var existingUsers = Users.ToList();
            //if (existingUsers.Any())
            //{
            //    Users.RemoveRange(existingUsers);
            //    Practitioners.RemoveRange(Practitioners);
            //    await SaveChangesAsync();
            //}

            // Seed roles
            var roles = new[] { "Admin", "User", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            //add an admin 
            string email = "admin@admin.com";
            string password = "admin123";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new ApplicationUser
                {
                    Email = email,
                    UserName = email
                };
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            
            // Seed practitioners as users
            var practitioners = new[]
            {
                new { Email = "practitioner1@example.com", FirstName = "Ivan", LastName = "Ivanov", Phone = "08888888888" },
                new { Email = "practitioner2@example.com", FirstName = "Dimitar", LastName = "Dimitrov", Phone = "+1949232323" },
            };

            foreach (var practitionerData in practitioners)
            {
                // Check if the user already exists
                var user = await userManager.FindByEmailAsync(practitionerData.Email);
                if (user == null)
                {
                    // Create ApplicationUser
                    user = new ApplicationUser { UserName = practitionerData.Email, Email = practitionerData.Email };
                    var result = await userManager.CreateAsync(user, "Password@123");

                    if (result.Succeeded)
                    {
                        // Assign the "User" role
                        await userManager.AddToRoleAsync(user, "User");

                        // Create and link Practitioner entity
                        var practitioner = new Practitioner
                        {
                            Id = user.Id,
                            FirstName = practitionerData.FirstName,
                            LastName = practitionerData.LastName,
                            Phone = practitionerData.Phone,
                        };

                        Practitioners.Add(practitioner);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create user {practitionerData.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            //Seed Specialties
            if (!context.Specialties.Any())
            {
                var specialties = new List<Specialty>
                {
                    new Specialty { Id = Guid.NewGuid(), Name = "Psychiatrist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Psychologist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Hypnotherapist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Speech therapist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Clinical psychologist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Group therapist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Family therapist" },
                    new Specialty { Id = Guid.NewGuid(), Name = "Psychotherapist" }
                };

                await context.Specialties.AddRangeAsync(specialties);
            }

            await SaveChangesAsync();
        }

    }
}
