using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMe.Data.Migrations
{
    public partial class AddFixFileInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileInfo_BaseAnswer_FileAnswerId",
                table: "FileInfo");

            migrationBuilder.DropIndex(
                name: "IX_FileInfo_FileAnswerId",
                table: "FileInfo");

            migrationBuilder.DropColumn(
                name: "FileAnswerId",
                table: "FileInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "FileInfoId",
                table: "BaseAnswer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseAnswer_FileInfoId",
                table: "BaseAnswer",
                column: "FileInfoId",
                unique: true,
                filter: "[FileInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseAnswer_FileInfo_FileInfoId",
                table: "BaseAnswer",
                column: "FileInfoId",
                principalTable: "FileInfo",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseAnswer_FileInfo_FileInfoId",
                table: "BaseAnswer");

            migrationBuilder.DropIndex(
                name: "IX_BaseAnswer_FileInfoId",
                table: "BaseAnswer");

            migrationBuilder.DropColumn(
                name: "FileInfoId",
                table: "BaseAnswer");

            migrationBuilder.AddColumn<Guid>(
                name: "FileAnswerId",
                table: "FileInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FileInfo_FileAnswerId",
                table: "FileInfo",
                column: "FileAnswerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileInfo_BaseAnswer_FileAnswerId",
                table: "FileInfo",
                column: "FileAnswerId",
                principalTable: "BaseAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
