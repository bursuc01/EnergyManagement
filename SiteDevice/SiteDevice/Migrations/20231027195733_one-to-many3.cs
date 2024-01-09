using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteDevice.Migrations
{
    /// <inheritdoc />
    public partial class onetomany3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user",
                table: "Devices",
                type: "integer",
                nullable: true);
        }
    }
}
