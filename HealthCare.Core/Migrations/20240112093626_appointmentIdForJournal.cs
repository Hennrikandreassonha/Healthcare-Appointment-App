﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Core.Migrations
{
    /// <inheritdoc />
    public partial class appointmentIdForJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "MedicalJournal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "MedicalJournal");
        }
    }
}
