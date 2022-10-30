using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBuildingPlatformAPI.Migrations
{
    public partial class ThirdTimesTheCharm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Conditions_ConditionId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Questions_QuestionId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Application_ApplicationId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Application_ApplicationId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_Application_QuestionId",
                table: "Applications",
                newName: "IX_Applications_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Application_ConditionId",
                table: "Applications",
                newName: "IX_Applications_ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Conditions_ConditionId",
                table: "Applications",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Questions_QuestionId",
                table: "Applications",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Applications_ApplicationId",
                table: "Categories",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Applications_ApplicationId",
                table: "Clients",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Conditions_ConditionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Questions_QuestionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Applications_ApplicationId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Applications_ApplicationId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_QuestionId",
                table: "Application",
                newName: "IX_Application_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ConditionId",
                table: "Application",
                newName: "IX_Application_ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Conditions_ConditionId",
                table: "Application",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Questions_QuestionId",
                table: "Application",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Application_ApplicationId",
                table: "Categories",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Application_ApplicationId",
                table: "Clients",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
