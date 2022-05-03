using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMe.Data.Migrations
{
    public partial class ChangeDateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Survey",
                newName: "LastChangeDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastChangeDate",
                table: "Survey",
                newName: "CreationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Survey",
                type: "datetime2",
                nullable: true);
        }
    }
}
