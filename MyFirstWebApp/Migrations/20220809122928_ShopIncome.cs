using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ShopIncome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sale",
                table: "Shops",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ShopIncome",
                table: "Shops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopIncome",
                table: "Shops");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sale",
                table: "Shops",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
