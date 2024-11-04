using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientManagementApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserDefined : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("0c0abce8-742b-414d-b13b-c7388c9089ac"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("2c1cde82-e26f-4948-8785-c735c451d38c"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("54936431-aaf6-4dbc-99a7-cf4e20cb7ece"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("68c02c89-f3a5-41a1-88bc-6a0b58b0fb50"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("982f9555-c950-419d-bd9b-e22e09671370"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("c93bb798-c580-4f96-a007-718003ff834e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("c9da2426-246a-4a2d-9dc0-aadcc8861ae1"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("e49a4f92-4367-4d31-b032-ce14e69f76e0"));

            migrationBuilder.AddColumn<Guid>(
                name: "PractitionerId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("047c2567-577a-4a3d-8519-73a5808b88d8"), "Family therapist" },
                    { new Guid("20fc9fae-db0f-40da-a3a1-1f28634eb5f6"), "Group therapist" },
                    { new Guid("21f1fe19-dffe-4aad-9b75-0ae340313e54"), "Psychotherapist" },
                    { new Guid("47ffffcd-600a-4c49-8d89-c660d51c076d"), "Speech therapist" },
                    { new Guid("9da3506e-6e19-425c-9876-f031cd354da3"), "Psychologist" },
                    { new Guid("a549671e-ee84-4d3d-9439-c649a9d72446"), "Clinical psychologist" },
                    { new Guid("d1beeb2e-9d18-4ce0-8c73-bdf4ea088c16"), "Hypnotherapist" },
                    { new Guid("fe8c4065-c407-47d4-92be-59c38aecd729"), "Psychiatrist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("047c2567-577a-4a3d-8519-73a5808b88d8"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("20fc9fae-db0f-40da-a3a1-1f28634eb5f6"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("21f1fe19-dffe-4aad-9b75-0ae340313e54"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("47ffffcd-600a-4c49-8d89-c660d51c076d"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("9da3506e-6e19-425c-9876-f031cd354da3"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a549671e-ee84-4d3d-9439-c649a9d72446"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("d1beeb2e-9d18-4ce0-8c73-bdf4ea088c16"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("fe8c4065-c407-47d4-92be-59c38aecd729"));

            migrationBuilder.DropColumn(
                name: "PractitionerId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c0abce8-742b-414d-b13b-c7388c9089ac"), "Group therapist" },
                    { new Guid("2c1cde82-e26f-4948-8785-c735c451d38c"), "Family therapist" },
                    { new Guid("54936431-aaf6-4dbc-99a7-cf4e20cb7ece"), "Clinical psychologist" },
                    { new Guid("68c02c89-f3a5-41a1-88bc-6a0b58b0fb50"), "Psychologist" },
                    { new Guid("982f9555-c950-419d-bd9b-e22e09671370"), "Psychiatrist" },
                    { new Guid("c93bb798-c580-4f96-a007-718003ff834e"), "Psychotherapist" },
                    { new Guid("c9da2426-246a-4a2d-9dc0-aadcc8861ae1"), "Hypnotherapist" },
                    { new Guid("e49a4f92-4367-4d31-b032-ce14e69f76e0"), "Speech therapist" }
                });
        }
    }
}
