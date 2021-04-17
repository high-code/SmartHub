using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Identity.Migrations.IdentityServerDb
{
    public partial class AddColumnsClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "client_name",
                table: "clients",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "client_id",
                table: "clients",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_client_id",
                table: "clients",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_user_id",
                table: "clients",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_clients_client_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_user_id",
                table: "clients");

            migrationBuilder.AlterColumn<string>(
                name: "client_name",
                table: "clients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "client_id",
                table: "clients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
