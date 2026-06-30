using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboard_api.Migrations
{
    /// <inheritdoc />
    public partial class AssessmentEnsure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "HomeAssistantToken", "HomeAssistantUrl" },
                values: new object[] { 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiIwNzc5NDdkMDAxNTc0OGQxYTVlZWU5NjFhNmI2OWUyYSIsImlhdCI6MTc4MTI4NjI3MCwiZXhwIjoyMDk2NjQ2MjcwfQ.G2UM-uBbmhsK8BTpIHLLto8bau6qsYGVhxGaprZbmSA", "http://192.168.2.28:8123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
