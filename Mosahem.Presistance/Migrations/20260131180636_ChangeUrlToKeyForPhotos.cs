using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mosahem.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUrlToKeyForPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImgUrl",
                table: "Volunteers",
                newName: "ProfileImgKey");

            migrationBuilder.RenameColumn(
                name: "CoverImgUrl",
                table: "Volunteers",
                newName: "CoverImgKey");

            migrationBuilder.RenameColumn(
                name: "LicenseUrl",
                table: "Organizations",
                newName: "LicenseKey");

            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Opportunities",
                newName: "PhotoKey");

            migrationBuilder.AddColumn<string>(
                name: "CVKey",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoKey",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVKey",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "LogoKey",
                table: "Organizations");

            migrationBuilder.RenameColumn(
                name: "ProfileImgKey",
                table: "Volunteers",
                newName: "ProfileImgUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImgKey",
                table: "Volunteers",
                newName: "CoverImgUrl");

            migrationBuilder.RenameColumn(
                name: "LicenseKey",
                table: "Organizations",
                newName: "LicenseUrl");

            migrationBuilder.RenameColumn(
                name: "PhotoKey",
                table: "Opportunities",
                newName: "LogoUrl");
        }
    }
}
