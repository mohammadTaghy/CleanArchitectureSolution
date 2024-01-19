using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Common_LessonsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IConPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Coefficient = table.Column<byte>(type: "tinyint", nullable: false),
                    CategoryType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LevelChar = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    AutoCode = table.Column<int>(type: "int", nullable: false),
                    FullKeyCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_LessonsCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_LessonsCategories_Common_LessonsCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Common_LessonsCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Membership_Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IConPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NumberCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LevelChar = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    AutoCode = table.Column<int>(type: "int", nullable: false),
                    FullKeyCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_Locations_Membership_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Membership_Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Membership_Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CommandName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    IConPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    FeatureType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LevelChar = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AutoCode = table.Column<int>(type: "int", nullable: false),
                    FullKeyCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_Permission_Membership_Permission_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Membership_Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Membership_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membership_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsMobileNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsUserConfirm = table.Column<bool>(type: "bit", nullable: false),
                    ManagerConfirm = table.Column<byte>(type: "tinyint", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membership_RolesPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_RolesPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_RolesPermission_Membership_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Membership_Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membership_RolesPermission_Membership_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Membership_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Common_UserEducation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LessonsCategoriesId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_UserEducation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_UserEducation_Common_LessonsCategories_LessonsCategoriesId",
                        column: x => x.LessonsCategoriesId,
                        principalTable: "Common_LessonsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Common_UserEducation_Membership_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Membership_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membership_UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PicturePath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EducationGrade = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    UserDescription = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_UserProfile_Membership_User_Id",
                        column: x => x.Id,
                        principalTable: "Membership_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membership_UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_UserRoles_Membership_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Membership_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membership_UserRoles_Membership_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Membership_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Common_LessonsCategories_ParentId",
                table: "Common_LessonsCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_UserEducation_LessonsCategoriesId",
                table: "Common_UserEducation",
                column: "LessonsCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_UserEducation_UserId",
                table: "Common_UserEducation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_Locations_ParentId",
                table: "Membership_Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_Permission_ParentId",
                table: "Membership_Permission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_RolesPermission_PermissionId",
                table: "Membership_RolesPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_RolesPermission_RoleId",
                table: "Membership_RolesPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_User_UserName",
                table: "Membership_User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Membership_UserRoles_RoleId",
                table: "Membership_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_UserRoles_UserId",
                table: "Membership_UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Common_UserEducation");

            migrationBuilder.DropTable(
                name: "Membership_Locations");

            migrationBuilder.DropTable(
                name: "Membership_RolesPermission");

            migrationBuilder.DropTable(
                name: "Membership_UserProfile");

            migrationBuilder.DropTable(
                name: "Membership_UserRoles");

            migrationBuilder.DropTable(
                name: "Common_LessonsCategories");

            migrationBuilder.DropTable(
                name: "Membership_Permission");

            migrationBuilder.DropTable(
                name: "Membership_Roles");

            migrationBuilder.DropTable(
                name: "Membership_User");
        }
    }
}
