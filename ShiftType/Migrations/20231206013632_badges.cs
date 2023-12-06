using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftType.Migrations
{
    /// <inheritdoc />
    public partial class badges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BadgeId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    FaIcon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BadgeId",
                table: "AspNetUsers",
                column: "BadgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Badge_BadgeId",
                table: "AspNetUsers",
                column: "BadgeId",
                principalTable: "Badge",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Badge_BadgeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BadgeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "AspNetUsers");
        }
    }
}
