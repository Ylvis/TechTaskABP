using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechicalTask.API.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X_Name",
                table: "Experiments");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "Experiments",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deviceToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Experiments",
                newName: "value");

            migrationBuilder.AddColumn<string>(
                name: "X_Name",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
