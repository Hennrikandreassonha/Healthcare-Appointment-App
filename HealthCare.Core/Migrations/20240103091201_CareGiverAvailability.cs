﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Core.Migrations
{
    /// <inheritdoc />
    public partial class CareGiverAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvaiable",
                table: "CareGiver",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvaiable",
                table: "CareGiver");
        }
    }
}
