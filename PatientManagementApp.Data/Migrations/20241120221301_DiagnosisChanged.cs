using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosisChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientsDiagnoses");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "The patient's diagnosis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Description of the diagnosis"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The patient's diagnosis")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientsDiagnoses",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsDiagnoses", x => new { x.PatientId, x.DiagnosisId });
                    table.ForeignKey(
                        name: "FK_PatientsDiagnoses_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientsDiagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDiagnoses_DiagnosisId",
                table: "PatientsDiagnoses",
                column: "DiagnosisId");
        }
    }
}
