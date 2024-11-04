using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientManagementApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppUserForeignKeyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10d27636-cabb-47f0-8b5d-a2a8bc0c7079"), "Psychologist" },
                    { new Guid("20e93830-4790-444c-82eb-d8f790a39566"), "Hypnotherapist" },
                    { new Guid("41a26781-459e-4c7a-8def-081767d81c83"), "Psychiatrist" },
                    { new Guid("77b1b8bb-02dc-4d0e-bbe4-e648ae78fc41"), "Family therapist" },
                    { new Guid("ecad484c-39b9-412b-9e1f-3ada69f3ce91"), "Group therapist" },
                    { new Guid("eccf7b2c-5b39-451b-bd6d-2f168d01f4d6"), "Clinical psychologist" },
                    { new Guid("f189639b-76c3-4a9e-9049-aba937485a66"), "Psychotherapist" },
                    { new Guid("fa31004c-59db-4a7a-acce-259376cfd9d5"), "Speech therapist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("10d27636-cabb-47f0-8b5d-a2a8bc0c7079"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("20e93830-4790-444c-82eb-d8f790a39566"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("41a26781-459e-4c7a-8def-081767d81c83"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("77b1b8bb-02dc-4d0e-bbe4-e648ae78fc41"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("ecad484c-39b9-412b-9e1f-3ada69f3ce91"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("eccf7b2c-5b39-451b-bd6d-2f168d01f4d6"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("f189639b-76c3-4a9e-9049-aba937485a66"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("fa31004c-59db-4a7a-acce-259376cfd9d5"));

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
    }
}
