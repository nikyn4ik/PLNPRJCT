using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdOrder",
                table: "Defects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_IdOrder",
                table: "Defects",
                column: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Orders_IdOrder",
                table: "Defects",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Orders_IdOrder",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_IdOrder",
                table: "Defects");

            migrationBuilder.AlterColumn<string>(
                name: "IdOrder",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
