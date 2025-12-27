using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App2025.Migrations
{
    /// <inheritdoc />
    public partial class adminusertbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });
            //seeding data using migration techniques (for login pupose
            migrationBuilder.InsertData(
              table: "AdminUsers",
              columns: new[] { "Id", "Name", "Password" },
              values: new object[] { 1, "admin", "admin123" }
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
