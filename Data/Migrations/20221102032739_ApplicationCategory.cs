using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ApplicationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Applications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Categories_CategoryId",
                table: "Applications",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Categories_CategoryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Applications");
        }
    }
}
