using System.Collections.Generic;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class NullableLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Application>>(
                name: "Applications",
                table: "Clients",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Application>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<Question>>(
                name: "Questions",
                table: "Categories",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Question>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<Application>>(
                name: "Applications",
                table: "Categories",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Application>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<Question>>(
                name: "Questions",
                table: "Applications",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Question>),
                oldType: "jsonb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Application>>(
                name: "Applications",
                table: "Clients",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Application>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Question>>(
                name: "Questions",
                table: "Categories",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Question>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Application>>(
                name: "Applications",
                table: "Categories",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Application>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Question>>(
                name: "Questions",
                table: "Applications",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Question>),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
