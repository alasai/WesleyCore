﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WesleyCore.User.Infrastructure.Migrations
{
    public partial class 创建表 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "System");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInfo_IsAdmin = table.Column<bool>(type: "bit", nullable: true),
                    UserInfo_Status = table.Column<int>(type: "int", nullable: true),
                    UserInfo_IDCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserInfo_ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserInfo_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserInfo_Memo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                schema: "System",
                table: "User",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                schema: "System",
                table: "User",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "System");
        }
    }
}