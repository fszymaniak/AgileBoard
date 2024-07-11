using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileBoard.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DraftEpic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Epics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Epics");
        }
    }
}
