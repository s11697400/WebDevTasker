using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setup.Migrations.Auth
{
    /// <inheritdoc />
    public partial class AcceptedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "accepted",
                table: "Friends",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted",
                table: "Friends");
        }
    }
}
