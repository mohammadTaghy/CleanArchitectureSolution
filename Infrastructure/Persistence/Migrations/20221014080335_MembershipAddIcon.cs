using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class MembershipAddIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_UserRoles",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_UserProfile",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_User",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_RolesPermission",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_Roles",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "IConPath",
                table: "Membership_Permission",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "Membership_Permission",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_UserRoles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_UserProfile");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_User");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_RolesPermission");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_Roles");

            migrationBuilder.DropColumn(
                name: "IConPath",
                table: "Membership_Permission");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Membership_Permission");
        }
    }
}
