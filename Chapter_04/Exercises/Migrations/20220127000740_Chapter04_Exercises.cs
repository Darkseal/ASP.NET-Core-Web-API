using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBGList.Migrations
{
    public partial class Chapter04_Exercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Flags",
                table: "Mechanic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Mechanic",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Flags",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Domains",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AlternateNames",
                table: "BoardGames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Designer",
                table: "BoardGames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Flags",
                table: "BoardGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flags",
                table: "Mechanic");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Mechanic");

            migrationBuilder.DropColumn(
                name: "Flags",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "AlternateNames",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "Designer",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "Flags",
                table: "BoardGames");
        }
    }
}
