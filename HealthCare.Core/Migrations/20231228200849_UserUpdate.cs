using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Core.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_CareGiver_CareGiverId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_CareGiverId",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "CareGiver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CareGiverId1",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CareGiverId1",
                table: "Appointment",
                column: "CareGiverId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_CareGiver_CareGiverId1",
                table: "Appointment",
                column: "CareGiverId1",
                principalTable: "CareGiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_CareGiver_CareGiverId1",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_CareGiverId1",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "CareGiver");

            migrationBuilder.DropColumn(
                name: "CareGiverId1",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CareGiverId",
                table: "Appointment",
                column: "CareGiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_CareGiver_CareGiverId",
                table: "Appointment",
                column: "CareGiverId",
                principalTable: "CareGiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
