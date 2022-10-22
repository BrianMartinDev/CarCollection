using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCollection.Migrations
{
    public partial class initialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Address", "Description", "Engine", "Name", "Year" },
                values: new object[] { 1, "Orlando, FL", "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.", "V8", "F-150", 2005 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Address", "Description", "Engine", "Name", "Year" },
                values: new object[] { 2, "Orlando, FL", "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.", "V8", "F-150", 2005 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Address", "Description", "Engine", "Name", "Year" },
                values: new object[] { 3, "Orlando, FL", "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.", "V8", "F-150", 2005 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
