﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientManagementApp.Data;

#nullable disable

namespace PatientManagementApp.Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Appointment description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("Appointment end time");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasComment("Appointment starting time");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("PractitionerId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Diagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Description of the diagnosis");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The patient's diagnosis");

                    b.HasKey("Id");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.EmergencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("The full name of the patient's emergency contact");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("The emergency contact's phone number");

                    b.Property<string>("Relationship")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("The emergency contact's relationship to the patient");

                    b.HasKey("Id");

                    b.ToTable("EmergencyContacts");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.FileUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Medication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("The description of the medication");

                    b.Property<decimal>("Dosage")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("The dosage per day");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("The name of the medication");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("The medication's producer");

                    b.HasKey("Id");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NoteText")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)")
                        .HasComment("The content of the note");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasComment("Patient's age");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasComment("Patient's date of birth");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("The patient's email address");

                    b.Property<Guid>("EmergencyContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Feedback from patient regarding the treatment");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Patient's first name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasComment("Patient's gender");

                    b.Property<string>("ImportantInfo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Important information about the patient");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("soft delete option");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasComment("Patient's last name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Patient's phone number");

                    b.Property<Guid>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReasonForVisit")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Initial reason for visiting");

                    b.Property<string>("ReferredBy")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Who referred this patient");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Whether the treatment is active in the moment");

                    b.Property<DateTime>("TreatmentEndDate")
                        .HasColumnType("datetime2")
                        .HasComment("Final date of the treatment");

                    b.Property<DateTime>("TreatmentStartDate")
                        .HasColumnType("datetime2")
                        .HasComment("The first date of the treatment");

                    b.HasKey("Id");

                    b.HasIndex("EmergencyContactId");

                    b.HasIndex("PractitionerId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PatientsDiagnoses", b =>
                {
                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiagnosisId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PatientId", "DiagnosisId");

                    b.HasIndex("DiagnosisId");

                    b.ToTable("PatientsDiagnoses");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PatientsMedications", b =>
                {
                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PatientId", "MedicationId");

                    b.HasIndex("MedicationId");

                    b.ToTable("PatientsMedications");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Practitioner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Practitioner's first name");

                    b.Property<bool>("IsAvailableOnline")
                        .HasColumnType("bit")
                        .HasComment("Whether the practitioner has online consultations with patients");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Practitioner's last name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Practitioner's phone number");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Practitioners");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PractitionersSpecialties", b =>
                {
                    b.Property<Guid>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PractitionerId", "SpecialtyId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("PractitionersSpecialties");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("The specialty name");

                    b.HasKey("Id");

                    b.ToTable("Specialties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a259caad-155f-4b28-854c-8144965dc0de"),
                            Name = "Psychiatrist"
                        },
                        new
                        {
                            Id = new Guid("98e9252f-6ccf-4d0e-aafd-9207fd55d6da"),
                            Name = "Psychologist"
                        },
                        new
                        {
                            Id = new Guid("0783493c-2c0f-41bf-8eea-8ef62b7615a2"),
                            Name = "Hypnotherapist"
                        },
                        new
                        {
                            Id = new Guid("1ce31ba5-842c-42b1-93f2-eb2f2589bc50"),
                            Name = "Speech therapist"
                        },
                        new
                        {
                            Id = new Guid("1f14d52f-d5ec-4f98-8098-3e42547c88bb"),
                            Name = "Clinical psychologist"
                        },
                        new
                        {
                            Id = new Guid("cc0107d7-1f4c-48b9-96f2-55c37e323144"),
                            Name = "Group therapist"
                        },
                        new
                        {
                            Id = new Guid("1d6f66e3-3368-47ec-8547-20eb30aa0063"),
                            Name = "Family therapist"
                        },
                        new
                        {
                            Id = new Guid("f0c872f5-cc28-450a-aaa2-e9d24a5b5487"),
                            Name = "Psychotherapist"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Appointment", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.Practitioner", "Practitioner")
                        .WithMany("Appointments")
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Practitioner");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.FileUpload", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Patient", "Patient")
                        .WithMany("Files")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Note", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Patient", "Patient")
                        .WithMany("Notes")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Patient", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.EmergencyContact", "EmergencyContact")
                        .WithMany()
                        .HasForeignKey("EmergencyContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.Practitioner", "Practitioner")
                        .WithMany("Patients")
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EmergencyContact");

                    b.Navigation("Practitioner");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PatientsDiagnoses", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Diagnosis", "Diagnosis")
                        .WithMany("PatientsDiagnoses")
                        .HasForeignKey("DiagnosisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.Patient", "Patient")
                        .WithMany("PatientsDiagnoses")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Diagnosis");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PatientsMedications", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Medication", "Medication")
                        .WithMany("PatientsMedications")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.Patient", "Patient")
                        .WithMany("PatientsMedications")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Practitioner", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.ApplicationUser", "User")
                        .WithOne("Practitioner")
                        .HasForeignKey("PatientManagementApp.Data.Models.Practitioner", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.PractitionersSpecialties", b =>
                {
                    b.HasOne("PatientManagementApp.Data.Models.Practitioner", "Practitioner")
                        .WithMany("PractitionersSpecialties")
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PatientManagementApp.Data.Models.Specialty", "Specialty")
                        .WithMany("PractitionersSpecialties")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Practitioner");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Practitioner")
                        .IsRequired();
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Diagnosis", b =>
                {
                    b.Navigation("PatientsDiagnoses");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Medication", b =>
                {
                    b.Navigation("PatientsMedications");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Files");

                    b.Navigation("Notes");

                    b.Navigation("PatientsDiagnoses");

                    b.Navigation("PatientsMedications");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Practitioner", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Patients");

                    b.Navigation("PractitionersSpecialties");
                });

            modelBuilder.Entity("PatientManagementApp.Data.Models.Specialty", b =>
                {
                    b.Navigation("PractitionersSpecialties");
                });
#pragma warning restore 612, 618
        }
    }
}
