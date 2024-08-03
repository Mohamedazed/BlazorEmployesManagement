using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthBlazer.Data.Migrations
{
    public partial class AddJoiningDateToEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoiningDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoiningDate",
                table: "Employees");
        }
    }
}
