using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class _002_tables_structure_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Participants");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "persons",
                type: "TEXT",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "companies",
                type: "TEXT",
                maxLength: 5000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "companies");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Participants",
                type: "TEXT",
                maxLength: 1500,
                nullable: true);
        }
    }
}
