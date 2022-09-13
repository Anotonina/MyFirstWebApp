using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cashiers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<string>(type: "TEXT", nullable: false),
                    ResourceName = table.Column<string>(type: "TEXT", nullable: true),
                    Parametrs = table.Column<string>(type: "TEXT", nullable: true),
                    TotalTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TotalCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cashiers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
