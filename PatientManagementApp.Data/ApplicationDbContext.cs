﻿using System.Reflection;
using System.Reflection.Emit;
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
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


        public async Task SeedData(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Starting SeedData method...");

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var existingUsers = Users.ToList();
            if (existingUsers.Any())
            {
                Console.WriteLine("Removing existing users and practitioners...");
                Users.RemoveRange(existingUsers);
                Practitioners.RemoveRange(Practitioners);
                await SaveChangesAsync();
            }

            // Seed roles
            var roles = new[] { "Admin", "User", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    Console.WriteLine($"Creating role: {role}");
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            // Seed practitioners as users

            Console.WriteLine("Seeding practitioners...");

                var practitioners = new[]
                {
                    new { Email = "practitioner1@example.com", FirstName = "Ivan", LastName = "Ivanov", Phone = "08888888888" },
                    new { Email = "practitioner2@example.com", FirstName = "Dimitar", LastName = "Dimitrov", Phone = "+1949232323" },
                };

                foreach (var practitionerData in practitioners)
                {
                    // Check if the practitioner user already exists
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
                                Id = Guid.NewGuid(),
                                FirstName = practitionerData.FirstName,
                                LastName = practitionerData.LastName,
                                Phone = practitionerData.Phone,
                                UserId = user.Id // Link to ApplicationUser ID
                            };

                            Practitioners.Add(practitioner);
                            Console.WriteLine($"Added practitioner: {practitioner.FirstName} {practitioner.LastName}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to create user {practitionerData.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            

            await SaveChangesAsync();
            Console.WriteLine("Finished seeding data.");
        }

    }
}
