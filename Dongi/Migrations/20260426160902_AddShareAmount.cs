using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dongi.Migrations
{
    /// <inheritdoc />
    public partial class AddShareAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShareAmount",
                table: "ExpensePersons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareAmount",
                table: "ExpensePersons");
        }
    }
}
