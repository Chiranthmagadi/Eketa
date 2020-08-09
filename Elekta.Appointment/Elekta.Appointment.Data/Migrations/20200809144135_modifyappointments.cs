using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elekta.Appointment.Data.Migrations
{
    public partial class modifyappointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Equipments_EquipmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Hospitals_HospitalId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_EquipmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_HospitalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "AppointmentDate",
                value: new DateTime(2020, 8, 9, 15, 41, 34, 698, DateTimeKind.Local).AddTicks(9240));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2020, 8, 9, 14, 49, 47, 747, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EquipmentId",
                table: "Appointments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HospitalId",
                table: "Appointments",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Equipments_EquipmentId",
                table: "Appointments",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Hospitals_HospitalId",
                table: "Appointments",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
