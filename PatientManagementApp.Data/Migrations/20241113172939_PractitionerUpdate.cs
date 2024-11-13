using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientManagementApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PractitionerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("0b5c1e59-02d7-45bc-bbfc-221c7f7b4b0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("2e99a57d-7d84-49b8-9b31-bddc3f30f17b"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("711c4934-5c6a-493d-956c-8d79fc04eb23"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("948e406e-b90e-4c75-ad20-20039cc92ca5"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a67f6bf3-7557-42f8-a6e8-26e8bddc465a"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("dd404980-12c2-4f32-98c3-08622a7db2d5"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("efb2acab-6ac1-4040-bb68-df7f25e364e7"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("fe9e54e0-bf72-4293-843a-526b4470fda2"));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Practitioners",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "Practitioner's phone number",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComment: "Practitioner's phone number");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Practitioners",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                comment: "Practitioner's last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldComment: "Practitioner's last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Practitioners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Practitioner's first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Practitioner's first name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Practitioners",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                comment: "Practitioner's phone number",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true,
                oldComment: "Practitioner's phone number");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Practitioners",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "Practitioner's last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true,
                oldComment: "Practitioner's last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Practitioners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Practitioner's first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Practitioner's first name");

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b5c1e59-02d7-45bc-bbfc-221c7f7b4b0e"), "Family therapist" },
                    { new Guid("2e99a57d-7d84-49b8-9b31-bddc3f30f17b"), "Clinical psychologist" },
                    { new Guid("711c4934-5c6a-493d-956c-8d79fc04eb23"), "Hypnotherapist" },
                    { new Guid("948e406e-b90e-4c75-ad20-20039cc92ca5"), "Psychiatrist" },
                    { new Guid("a67f6bf3-7557-42f8-a6e8-26e8bddc465a"), "Psychologist" },
                    { new Guid("dd404980-12c2-4f32-98c3-08622a7db2d5"), "Psychotherapist" },
                    { new Guid("efb2acab-6ac1-4040-bb68-df7f25e364e7"), "Speech therapist" },
                    { new Guid("fe9e54e0-bf72-4293-843a-526b4470fda2"), "Group therapist" }
                });
        }
    }
}
