using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class _003_table_structure_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_Participants_Id",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_event_participants_Participants_ParticipantId",
                table: "event_participants");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_Participants_Id",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "participant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_participant",
                table: "participant",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_participant_Code",
                table: "participant",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_companies_participant_Id",
                table: "companies",
                column: "Id",
                principalTable: "participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_participants_participant_ParticipantId",
                table: "event_participants",
                column: "ParticipantId",
                principalTable: "participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_participant_Id",
                table: "persons",
                column: "Id",
                principalTable: "participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_participant_Id",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_event_participants_participant_ParticipantId",
                table: "event_participants");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_participant_Id",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_participant",
                table: "participant");

            migrationBuilder.DropIndex(
                name: "IX_participant_Code",
                table: "participant");

            migrationBuilder.RenameTable(
                name: "participant",
                newName: "Participants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_companies_Participants_Id",
                table: "companies",
                column: "Id",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_participants_Participants_ParticipantId",
                table: "event_participants",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_Participants_Id",
                table: "persons",
                column: "Id",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
