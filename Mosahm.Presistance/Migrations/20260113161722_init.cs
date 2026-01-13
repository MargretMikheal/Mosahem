using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mosahm.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AuthProvider = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GovernorateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LicenseUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VerificationComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    ProfileImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalHours = table.Column<int>(type: "int", nullable: false),
                    CompleteOpportunities = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descripition = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vacancies = table.Column<int>(type: "int", nullable: false),
                    ApplicantsCount = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationFields",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationFields", x => new { x.OrganizationId, x.FieldId });
                    table.ForeignKey(
                        name: "FK_OrganizationFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationFields_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationFollowers",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationFollowers", x => new { x.VolunteerId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationFollowers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationFollowers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerFields",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerFields", x => new { x.VolunteerId, x.FieldId });
                    table.ForeignKey(
                        name: "FK_VolunteerFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerFields_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerSkills",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerSkills", x => new { x.VolunteerId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_VolunteerSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerSkills_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityApplications_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpportunityApplications_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityComments_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityComments_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityFields",
                columns: table => new
                {
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityFields", x => new { x.OpportunityId, x.FieldId });
                    table.ForeignKey(
                        name: "FK_OpportunityFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpportunityFields_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityLikes",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityLikes", x => new { x.VolunteerId, x.OpportunityId });
                    table.ForeignKey(
                        name: "FK_OpportunityLikes_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityLikes_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunitySaves",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunitySaves", x => new { x.VolunteerId, x.OpportunityId });
                    table.ForeignKey(
                        name: "FK_OpportunitySaves_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunitySaves_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunitySkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunitySkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunitySkills_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpportunitySkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AnswerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ChoiceKey = table.Column<int>(type: "int", nullable: true),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OpportunityId",
                table: "Addresses",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                table: "Addresses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_VolunteerId",
                table: "Addresses",
                column: "VolunteerId",
                unique: true,
                filter: "[VolunteerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernorateId",
                table: "Cities",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId",
                table: "Opportunities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityApplications_OpportunityId",
                table: "OpportunityApplications",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityApplications_VolunteerId",
                table: "OpportunityApplications",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityComments_OpportunityId",
                table: "OpportunityComments",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityComments_VolunteerId",
                table: "OpportunityComments",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityFields_FieldId",
                table: "OpportunityFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityLikes_OpportunityId",
                table: "OpportunityLikes",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunitySaves_OpportunityId",
                table: "OpportunitySaves",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunitySkills_OpportunityId_SkillId_SkillType",
                table: "OpportunitySkills",
                columns: new[] { "OpportunityId", "SkillId", "SkillType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpportunitySkills_SkillId",
                table: "OpportunitySkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationFields_FieldId",
                table: "OrganizationFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationFollowers_OrganizationId",
                table: "OrganizationFollowers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionId",
                table: "QuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_VolunteerId",
                table: "QuestionAnswers",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_OpportunityId_Order",
                table: "Questions",
                columns: new[] { "OpportunityId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerFields_FieldId",
                table: "VolunteerFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerFields_VolunteerId",
                table: "VolunteerFields",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_NationalId",
                table: "Volunteers",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSkills_SkillId",
                table: "VolunteerSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSkills_VolunteerId",
                table: "VolunteerSkills",
                column: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OpportunityApplications");

            migrationBuilder.DropTable(
                name: "OpportunityComments");

            migrationBuilder.DropTable(
                name: "OpportunityFields");

            migrationBuilder.DropTable(
                name: "OpportunityLikes");

            migrationBuilder.DropTable(
                name: "OpportunitySaves");

            migrationBuilder.DropTable(
                name: "OpportunitySkills");

            migrationBuilder.DropTable(
                name: "OrganizationFields");

            migrationBuilder.DropTable(
                name: "OrganizationFollowers");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "VolunteerFields");

            migrationBuilder.DropTable(
                name: "VolunteerSkills");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
