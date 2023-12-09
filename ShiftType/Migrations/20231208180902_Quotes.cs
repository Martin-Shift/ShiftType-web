using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftType.Migrations
{
    /// <inheritdoc />
    public partial class Quotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Quotes_QuoteId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Reviews",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Quotes_QuoteId",
                table: "Reviews",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Quotes_QuoteId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Quotes_QuoteId",
                table: "Reviews",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
