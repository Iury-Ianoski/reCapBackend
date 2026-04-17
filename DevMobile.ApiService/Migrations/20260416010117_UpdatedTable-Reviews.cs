using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevMobile.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "Reviews",
                newName: "InitialChapter");

            migrationBuilder.AddColumn<int>(
                name: "FinalChapter",
                table: "Reviews",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalChapter",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "InitialChapter",
                table: "Reviews",
                newName: "Chapter");
        }
    }
}
