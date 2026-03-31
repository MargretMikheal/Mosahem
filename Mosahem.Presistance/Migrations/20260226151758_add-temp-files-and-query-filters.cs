using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mosahem.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class addtempfilesandqueryfilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporaryFileUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileKey = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryFileUploads", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryFileUploads_FileKey",
                table: "TemporaryFileUploads",
                column: "FileKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryFileUploads");
        }
    }
}
