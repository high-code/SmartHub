using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Identity.Migrations
{
    public partial class AddFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idn",
                table: "roles",
                keyColumns: new[] { "id", "concurrency_stamp" },
                keyValues: new object[] { "d509c4f7-8e14-4e40-b468-0f2e8c82df44", "bffc9295-9851-4787-9543-f32757b12996" });

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                schema: "idn",
                table: "users",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "idn",
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "bed79545-4c6e-4a9d-aa53-d99681386f37", "03fb8ee4-f545-47ec-9d56-c18bdf0f776d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idn",
                table: "roles",
                keyColumns: new[] { "id", "concurrency_stamp" },
                keyValues: new object[] { "bed79545-4c6e-4a9d-aa53-d99681386f37", "03fb8ee4-f545-47ec-9d56-c18bdf0f776d" });

            migrationBuilder.DropColumn(
                name: "full_name",
                schema: "idn",
                table: "users");

            migrationBuilder.InsertData(
                schema: "idn",
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "d509c4f7-8e14-4e40-b468-0f2e8c82df44", "bffc9295-9851-4787-9543-f32757b12996", "Admin", "ADMIN" });
        }
    }
}
