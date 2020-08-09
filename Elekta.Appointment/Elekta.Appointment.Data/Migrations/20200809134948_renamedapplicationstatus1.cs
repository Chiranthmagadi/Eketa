using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elekta.Appointment.Data.Migrations
{
    public partial class renamedapplicationstatus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "BookingDate", "EquipmentId", "HospitalId", "ModifiedDate", "PatientId", "Status" },
                values: new object[] { 1, new DateTime(2020, 8, 9, 14, 49, 47, 747, DateTimeKind.Local).AddTicks(246), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Booked" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);
        }
    }
}
