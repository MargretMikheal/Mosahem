using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mosahem.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class addaboutustoorganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "Organizations");
        }
    }
}
