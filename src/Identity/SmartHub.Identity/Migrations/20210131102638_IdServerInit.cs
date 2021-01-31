using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Identity.Migrations
{
    public partial class IdServerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idn",
                table: "roles",
                keyColumns: new[] { "id", "concurrency_stamp" },
                keyValues: new object[] { "bed79545-4c6e-4a9d-aa53-d99681386f37", "03fb8ee4-f545-47ec-9d56-c18bdf0f776d" });

            migrationBuilder.InsertData(
                schema: "idn",
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "09fe8272-cdff-489f-89f2-7303deb389dc", "9576198b-6db3-469c-8636-9d4c565bf8d4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idn",
                table: "roles",
                keyColumns: new[] { "id", "concurrency_stamp" },
                keyValues: new object[] { "09fe8272-cdff-489f-89f2-7303deb389dc", "9576198b-6db3-469c-8636-9d4c565bf8d4" });

            migrationBuilder.InsertData(
                schema: "idn",
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "bed79545-4c6e-4a9d-aa53-d99681386f37", "03fb8ee4-f545-47ec-9d56-c18bdf0f776d", "Admin", "ADMIN" });
        }
    }
}
