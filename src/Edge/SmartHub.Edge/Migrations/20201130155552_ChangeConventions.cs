using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Edge.Migrations
{
    public partial class ChangeConventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationEventLog",
                table: "IntegrationEventLog");

            migrationBuilder.RenameTable(
                name: "IntegrationEventLog",
                newName: "integration_event_log");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "integration_event_log",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "integration_event_log",
                newName: "transaction_id");

            migrationBuilder.RenameColumn(
                name: "EventTypeName",
                table: "integration_event_log",
                newName: "event_type_name");

            migrationBuilder.RenameColumn(
                name: "EventState",
                table: "integration_event_log",
                newName: "event_state");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "integration_event_log",
                newName: "created_time");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "integration_event_log",
                newName: "event_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_integration_event_log",
                table: "integration_event_log",
                column: "event_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_integration_event_log",
                table: "integration_event_log");

            migrationBuilder.RenameTable(
                name: "integration_event_log",
                newName: "IntegrationEventLog");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "IntegrationEventLog",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "IntegrationEventLog",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "event_type_name",
                table: "IntegrationEventLog",
                newName: "EventTypeName");

            migrationBuilder.RenameColumn(
                name: "event_state",
                table: "IntegrationEventLog",
                newName: "EventState");

            migrationBuilder.RenameColumn(
                name: "created_time",
                table: "IntegrationEventLog",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "event_id",
                table: "IntegrationEventLog",
                newName: "EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationEventLog",
                table: "IntegrationEventLog",
                column: "EventId");
        }
    }
}
