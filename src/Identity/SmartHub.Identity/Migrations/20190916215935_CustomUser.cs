using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Identity.Migrations
{
    public partial class CustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "idn",
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "d509c4f7-8e14-4e40-b468-0f2e8c82df44", "bffc9295-9851-4787-9543-f32757b12996", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idn",
                table: "roles",
                keyColumns: new[] { "id", "concurrency_stamp" },
                keyValues: new object[] { "d509c4f7-8e14-4e40-b468-0f2e8c82df44", "bffc9295-9851-4787-9543-f32757b12996" });
        }
    }
}
