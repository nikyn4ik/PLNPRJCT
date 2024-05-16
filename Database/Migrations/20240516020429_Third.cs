using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarkPackage",
                table: "Container",
                newName: "MarkContainer");

            migrationBuilder.AlterColumn<string>(
                name: "IdOrder",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DTContainer",
                table: "Container",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Container",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DTContainer",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "MarkContainer",
                table: "Container",
                newName: "MarkPackage");

            migrationBuilder.AlterColumn<string>(
                name: "IdOrder",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
