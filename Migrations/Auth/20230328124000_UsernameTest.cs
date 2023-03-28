using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setup.Migrations.Auth
{
    /// <inheritdoc />
    public partial class UsernameTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName1",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName2",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName1",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserName2",
                table: "Friends");
        }
    }
}
