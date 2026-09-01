using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Distribuidora.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixFixFirstNameColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstame",
                table: "Users",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Firstame");
        }
    }
}
