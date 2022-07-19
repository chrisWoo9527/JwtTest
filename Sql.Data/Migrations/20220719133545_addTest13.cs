using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTest13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Test_Table",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Test_Table");
        }
    }
}
