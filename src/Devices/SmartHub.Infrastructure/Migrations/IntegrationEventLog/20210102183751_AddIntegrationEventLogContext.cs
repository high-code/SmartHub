using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations.IntegrationEventLog
{
    public partial class AddIntegrationEventLogContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "integration_event_log",
                columns: table => new
                {
                    event_id = table.Column<Guid>(nullable: false),
                    event_type_name = table.Column<string>(nullable: false),
                    event_state = table.Column<int>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false),
                    content = table.Column<string>(nullable: false),
                    transaction_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_integration_event_log", x => x.event_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "integration_event_log");
        }
    }
}
