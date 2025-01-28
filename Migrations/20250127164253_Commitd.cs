using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryApi.Migrations
{
    /// <inheritdoc />
    public partial class Commitd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isAvailable",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "Books");
        }
    }
}
