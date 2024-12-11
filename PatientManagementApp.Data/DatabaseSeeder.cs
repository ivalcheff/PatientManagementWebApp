using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientManagementApp.Data.Models;

namespace PatientManagementApp.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DatabaseSeeder(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedAdminAsync();
            var specialties = await SeedSpecialtiesAsync();
            var medications = await SeedMedicationsAsync();

            await SeedPractitionersAndPatientsAsync(specialties);
            await _context.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "User", "Client" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        private async Task SeedAdminAsync()
        {
            string email = "admin@admin.com";
            string password = "admin123";

            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var admin = new ApplicationUser { Email = email, UserName = email };
                await _userManager.CreateAsync(admin, password);
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        private async Task<List<Specialty>> SeedSpecialtiesAsync()
        {
            if (!_context.Specialties.Any())
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
                await _context.Specialties.AddRangeAsync(specialties);
                return specialties;
            }

            return await _context.Specialties.ToListAsync();
        }

        private async Task<List<Medication>> SeedMedicationsAsync()
        {
            if (!_context.Medications.Any())
            {
                var medications = new List<Medication>
                {
                    new Medication { Id = Guid.NewGuid(), Name = "Sertraline", Description = "Used to treat depression and anxiety disorders", Producer = "Pfizer", Dosage = 50m },
                    new Medication { Id = Guid.NewGuid(), Name = "Fluoxetine", Description = "Used to treat depression, OCD, and panic disorders", Producer = "Eli Lilly", Dosage = 20m },
                    new Medication { Id = Guid.NewGuid(), Name = "Escitalopram", Description = "Used to treat anxiety and major depressive disorder", Producer = "Forest Pharmaceuticals", Dosage = 10m },
                    new Medication { Id = Guid.NewGuid(), Name = "Risperidone", Description = "Used to treat schizophrenia and bipolar disorder", Producer = "Janssen", Dosage = 2m },
                    new Medication { Id = Guid.NewGuid(), Name = "Aripiprazole", Description = "Used to treat schizophrenia and bipolar disorder", Producer = "Otsuka", Dosage = 15m },
                    new Medication { Id = Guid.NewGuid(), Name = "Olanzapine", Description = "Used to treat schizophrenia and bipolar disorder", Producer = "Eli Lilly", Dosage = 10m }
                };

                await _context.Medications.AddRangeAsync(medications);
                return medications;
            }

            return await _context.Medications.ToListAsync();
        }

        private async Task SeedPractitionersAndPatientsAsync(List<Specialty> specialties)
        {
            var practitionerData = new[]
            {
                new { Email = "practitioner1@example.com", FirstName = "Ivan", LastName = "Ivanov", Phone = "08888888888" },
                new { Email = "practitioner2@example.com", FirstName = "Dimitar", LastName = "Dimitrov", Phone = "+1949232323" }
            };

            foreach (var data in practitionerData)
            {
                // Ensure the user exists
                var user = await _userManager.FindByEmailAsync(data.Email);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = data.Email, Email = data.Email };
                    await _userManager.CreateAsync(user, "Password@123");
                    await _userManager.AddToRoleAsync(user, "User");
                }

                // Ensure a Practitioner is linked to the user
                var practitionerExists = await _context.Practitioners.AnyAsync(p => p.Id == user.Id);
                if (!practitionerExists)
                {
                    var practitioner = new Practitioner
                    {
                        Id = user.Id,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Phone = data.Phone
                    };

                    // Assign specialties
                    var practitionerSpecialties = specialties
                        .OrderBy(_ => Guid.NewGuid())
                        .Take(3)
                        .Select(s => new PractitionersSpecialties
                        {
                            PractitionerId = practitioner.Id,
                            SpecialtyId = s.Id
                        }).ToList();

                    _context.Practitioners.Add(practitioner);
                    _context.PractitionersSpecialties.AddRange(practitionerSpecialties);

                    // Seed patients for this practitioner
                    await SeedPatientsAsync(practitioner.Id);
                }
            }
        }

        private async Task SeedPatientsAsync(Guid practitionerId)
        {

            for (int i = 1; i <= 10; i++)
            {
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"PatientFirst{i}",
                    LastName = $"PatientLast{i}",
                    Age = 25 + i,
                    PhoneNumber = $"123-456-789{i}",
                    PractitionerId = practitionerId,
                    ImportantInfo = "something very important to know",
                    IsActive = true,
                    EmergencyContact = new EmergencyContact
                    {
                        Id = Guid.NewGuid(),
                        Name = $"EmergencyContact{i}",
                        PhoneNumber = $"987-654-321{i}",
                        Relationship = "Parent",
                        IsDeleted = false
                    }
                };

                var appointments = Enumerable.Range(1, 4).Select(j => new Appointment
                {
                    Description = $"Appointment {j}",
                    StartDate = DateTime.UtcNow.AddDays(j),
                    EndDate = DateTime.UtcNow.AddDays(j).AddHours(1),
                    PractitionerId = practitionerId,
                    PatientId = patient.Id
                }).ToList();

                _context.Patients.Add(patient);
                _context.Appointments.AddRange(appointments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
