using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboard_api.Migrations
{
    /// <inheritdoc />
    public partial class deploymentpatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "HomeAssistantUrl",
                value: "http://192.168.2.29:8123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "HomeAssistantUrl",
                value: "http://192.168.2.28:8123");
        }
    }
}
