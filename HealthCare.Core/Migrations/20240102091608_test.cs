using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Core.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareGiverNotes",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "CareGiverNotes",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
