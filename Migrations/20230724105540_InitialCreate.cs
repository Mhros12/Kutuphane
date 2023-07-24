using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kutuphane.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedKitapID",
                table: "Kitapliklar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedKitapID",
                table: "Kitapliklar",
                type: "int",
                nullable: true);
        }
    }
}
