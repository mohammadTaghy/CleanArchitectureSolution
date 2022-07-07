using Common;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Migrations
{
    internal class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new {
                    Id = table.Column<int>(nullable: false,type:SqlDataTypeConstant.Int)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false, type: SqlDataTypeConstant.NVarChar, maxLength: 1024),
                    LastName = table.Column<string>(nullable: false, type: SqlDataTypeConstant.NVarChar, maxLength: 1024),
                    MobileNumber = table.Column<string>(nullable: false, type: SqlDataTypeConstant.VarChar, maxLength: 12),
                    UserName = table.Column<string>(nullable: false, type: SqlDataTypeConstant.VarChar, maxLength: 512),
                    UserCode = table.Column<string>(nullable: true, type: SqlDataTypeConstant.VarChar, maxLength: 10),
                    NationalCode = table.Column<string>(nullable: true, type: SqlDataTypeConstant.VarChar, maxLength: 10),
                    CreateBy = table.Column<string>(nullable: false, type: SqlDataTypeConstant.Int),
                    CreateDate = table.Column<string>(nullable: false, type: SqlDataTypeConstant.DateTime),
                    ModifyBy = table.Column<string>(nullable: true, type: SqlDataTypeConstant.Int),
                    ModifyDate = table.Column<string>(nullable: true, type: SqlDataTypeConstant.DateTime),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            base.Down(migrationBuilder);
        }
    }
}
