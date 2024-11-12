using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientManagementApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PractitionerIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Remove the dependent data from Practitioners
            migrationBuilder.Sql("DELETE FROM Practitioners");

            //Remove all existing users from AspNetUsers
            migrationBuilder.Sql("DELETE FROM AspNetUsers");

            // Drop foreign keys before making changes
            migrationBuilder.DropForeignKey(
                name: "FK_Practitioners_AspNetUsers_UserId",
                table: "Practitioners");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUsers_Practitioners_PractitionerId",
            //    table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Practitioners_UserId",
                table: "Practitioners");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Practitioners");

            // Alter Practitioner table columns
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Practitioners",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                comment: "Practitioner's last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Practitioner's last name");

            // Alter Patient table columns
            migrationBuilder.AlterColumn<string>(
                name: "ReferredBy",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Who referred this patient",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Who referred this patient");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonForVisit",
                table: "Patients",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Initial reason for visiting",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Initial reason for visiting");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Patients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Feedback from patient regarding the treatment",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Feedback from patient regarding the treatment");

            // Alter Medication table columns
            migrationBuilder.AlterColumn<decimal>(
                name: "Dosage",
                table: "Medications",
                type: "decimal(10,2)",
                nullable: false,
                comment: "The dosage per day",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The dosage per day");

            // Create a new index in AspNetUsers on PractitionerId
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PractitionerId",
                table: "AspNetUsers",
                column: "PractitionerId");

            //// Add the foreign key relationship from AspNetUsers to Practitioners
            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUsers_Practitioners_PractitionerId",
            //    table: "AspNetUsers",
            //    column: "PractitionerId",
            //    principalTable: "Practitioners",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            // Add the foreign key relationship from Practitioners to AspNetUsers
            migrationBuilder.AddForeignKey(
                name: "FK_Practitioners_AspNetUsers_Id",
                table: "Practitioners",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //// Drop foreign keys
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUsers_Practitioners_PractitionerId",
            //    table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Practitioners_AspNetUsers_Id",
                table: "Practitioners");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PractitionerId",
                table: "AspNetUsers");

            // Reverse the changes made to columns
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Practitioners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Practitioner's last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldComment: "Practitioner's last name");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Practitioners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ReferredBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Who referred this patient",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Who referred this patient");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonForVisit",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Initial reason for visiting",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Initial reason for visiting");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Feedback from patient regarding the treatment",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Feedback from patient regarding the treatment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Dosage",
                table: "Medications",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The dosage per day",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldComment: "The dosage per day");

            // Re-create the index and foreign key from Practitioners to AspNetUsers
            migrationBuilder.CreateIndex(
                name: "IX_Practitioners_UserId",
                table: "Practitioners",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Practitioners_AspNetUsers_UserId",
                table: "Practitioners",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
