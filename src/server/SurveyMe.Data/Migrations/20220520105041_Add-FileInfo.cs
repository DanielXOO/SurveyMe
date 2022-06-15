using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMe.Data.Migrations
{
    public partial class AddFileInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_Survey_SurveyId",
                table: "SurveyAnswer");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "BaseAnswer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "SurveyAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileInfo_BaseAnswer_FileAnswerId",
                        column: x => x.FileAnswerId,
                        principalTable: "BaseAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInfo_FileAnswerId",
                table: "FileInfo",
                column: "FileAnswerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_Survey_SurveyId",
                table: "SurveyAnswer",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_Survey_SurveyId",
                table: "SurveyAnswer");

            migrationBuilder.DropTable(
                name: "FileInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "SurveyAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "BaseAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_Survey_SurveyId",
                table: "SurveyAnswer",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id");
        }
    }
}
