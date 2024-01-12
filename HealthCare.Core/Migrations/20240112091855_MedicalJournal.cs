using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Core.Migrations
{
    /// <inheritdoc />
    public partial class MedicalJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalJournalEntry_CareGiver_CareGiverId",
                table: "MedicalJournalEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalJournalEntry_Patient_PatientId",
                table: "MedicalJournalEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalJournalEntry",
                table: "MedicalJournalEntry");

            migrationBuilder.RenameTable(
                name: "MedicalJournalEntry",
                newName: "MedicalJournal");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalJournalEntry_PatientId",
                table: "MedicalJournal",
                newName: "IX_MedicalJournal_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalJournalEntry_CareGiverId",
                table: "MedicalJournal",
                newName: "IX_MedicalJournal_CareGiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalJournal",
                table: "MedicalJournal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalJournal_CareGiver_CareGiverId",
                table: "MedicalJournal",
                column: "CareGiverId",
                principalTable: "CareGiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalJournal_Patient_PatientId",
                table: "MedicalJournal",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalJournal_CareGiver_CareGiverId",
                table: "MedicalJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalJournal_Patient_PatientId",
                table: "MedicalJournal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalJournal",
                table: "MedicalJournal");

            migrationBuilder.RenameTable(
                name: "MedicalJournal",
                newName: "MedicalJournalEntry");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalJournal_PatientId",
                table: "MedicalJournalEntry",
                newName: "IX_MedicalJournalEntry_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalJournal_CareGiverId",
                table: "MedicalJournalEntry",
                newName: "IX_MedicalJournalEntry_CareGiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalJournalEntry",
                table: "MedicalJournalEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalJournalEntry_CareGiver_CareGiverId",
                table: "MedicalJournalEntry",
                column: "CareGiverId",
                principalTable: "CareGiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalJournalEntry_Patient_PatientId",
                table: "MedicalJournalEntry",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
