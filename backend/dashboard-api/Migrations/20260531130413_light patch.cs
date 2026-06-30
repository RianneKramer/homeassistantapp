using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboard_api.Migrations
{
    /// <inheritdoc />
    public partial class lightpatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOn",
                table: "Lights");

            migrationBuilder.RenameColumn(
                name: "WebHook",
                table: "Lights",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Lights",
                newName: "WebHook");

            migrationBuilder.AddColumn<bool>(
                name: "IsOn",
                table: "Lights",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
