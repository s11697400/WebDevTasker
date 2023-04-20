using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setup.Migrations.Auth
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Friends",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { 874, "seedmail@gmail.com", "Test123", "Person Seed" });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_PersonId",
                table: "Friends",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Person_PersonId",
                table: "Friends",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Person_PersonId",
                table: "Friends");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Friends_PersonId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Friends");
        }
    }
}
