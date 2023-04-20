using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setup.Migrations.Auth
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { 454, "seedmail@gmail.com", "Test123", "Person Seed" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "2758afa0-f909-4443-b838-d324486ad9ea" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "2758afa0-f909-4443-b838-d324486ad9ea" });

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { 874, "seedmail@gmail.com", "Test123", "Person Seed" });
        }
    }
}
