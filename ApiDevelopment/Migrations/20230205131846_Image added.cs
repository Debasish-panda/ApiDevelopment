using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDevelopment.Migrations
{
    /// <inheritdoc />
    public partial class Imageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "students",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "students");
        }
    }
}
