using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ChangeUserProfileRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_User_Id",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserProfile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifyBy",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "UserProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_User_Id",
                table: "UserProfile",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_User_Id",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_User_Id",
                table: "UserProfile",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
