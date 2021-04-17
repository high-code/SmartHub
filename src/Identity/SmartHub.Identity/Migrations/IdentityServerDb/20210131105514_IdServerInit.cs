using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartHub.Identity.Migrations.IdentityServerDb
{
    public partial class IdServerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    client_id = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    client_name = table.Column<string>(nullable: true),
                    allow_access_tokens_via_browser = table.Column<bool>(nullable: false),
                    require_consent = table.Column<bool>(nullable: false),
                    always_include_user_claims_in_id_token = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client_grant_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    grant_type = table.Column<string>(maxLength: 250, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_grant_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_grant_types_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_postlogout_redirect_uris",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    post_logout_redirect_uri = table.Column<string>(maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_postlogout_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_postlogout_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_redirect_uris",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    redirect_uri = table.Column<string>(maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_scopes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    scope = table.Column<string>(maxLength: 250, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_scopes_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_secrets",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    client_id = table.Column<int>(nullable: false),
                    value = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_secrets", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_secrets_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_client_grant_types_client_id",
                table: "client_grant_types",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_postlogout_redirect_uris_client_id",
                table: "client_postlogout_redirect_uris",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_redirect_uris_client_id",
                table: "client_redirect_uris",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_scopes_client_id",
                table: "client_scopes",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_secrets_client_id",
                table: "client_secrets",
                column: "client_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_grant_types");

            migrationBuilder.DropTable(
                name: "client_postlogout_redirect_uris");

            migrationBuilder.DropTable(
                name: "client_redirect_uris");

            migrationBuilder.DropTable(
                name: "client_scopes");

            migrationBuilder.DropTable(
                name: "client_secrets");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
