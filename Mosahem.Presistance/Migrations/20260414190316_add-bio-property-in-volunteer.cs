using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mosahem.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class addbiopropertyinvolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Volunteers");
        }
    }
}
