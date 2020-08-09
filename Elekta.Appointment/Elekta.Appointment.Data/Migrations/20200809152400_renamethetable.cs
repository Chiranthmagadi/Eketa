using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elekta.Appointment.Data.Migrations
{
    public partial class renamethetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Appointments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "HospitalId", "Address", "HospitalName" },
                values: new object[,]
                {
                    { 1, "Woolwich", "QE" },
                    { 2, "London", "GH" },
                    { 3, "London", "Test" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "PatientEmailId", "PatientName" },
                values: new object[,]
                {
                    { 1, "george@test.com", "George" },
                    { 2, "json@test.com", "Json" },
                    { 3, "luci@test.com", "Luci" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "HospitalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "HospitalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "HospitalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "PatientId", "Status" },
                values: new object[] { 1, new DateTime(2020, 8, 9, 15, 41, 34, 698, DateTimeKind.Local).AddTicks(9240), null, "Booked" });
        }
    }
}
