﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WesleyCore.User.Infrastructure.Migrations
{
    public partial class 创建表1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllowUserNumber = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberShip",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberShip_Member_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "System",
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_PhoneNumber",
                schema: "System",
                table: "Member",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShip_MemberId",
                schema: "System",
                table: "MemberShip",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberShip",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Member",
                schema: "System");
        }
    }
}