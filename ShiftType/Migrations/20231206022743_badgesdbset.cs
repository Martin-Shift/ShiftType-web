using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftType.Migrations
{
    /// <inheritdoc />
    public partial class badgesdbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Badge_BadgeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badge",
                table: "Badge");

            migrationBuilder.RenameTable(
                name: "Badge",
                newName: "Badges");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Badges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badges",
                table: "Badges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Badges_BadgeId",
                table: "AspNetUsers",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Badges_BadgeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badges",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Badges");

            migrationBuilder.RenameTable(
                name: "Badges",
                newName: "Badge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badge",
                table: "Badge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Badge_BadgeId",
                table: "AspNetUsers",
                column: "BadgeId",
                principalTable: "Badge",
                principalColumn: "Id");
        }
    }
}
