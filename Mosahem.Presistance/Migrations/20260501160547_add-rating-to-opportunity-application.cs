using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mosahem.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class addratingtoopportunityapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "OpportunityApplications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "OpportunityApplications");
        }
    }
}
