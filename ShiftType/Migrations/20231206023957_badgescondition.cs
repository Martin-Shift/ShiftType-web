using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftType.Migrations
{
    /// <inheritdoc />
    public partial class badgescondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Badges");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Badges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
