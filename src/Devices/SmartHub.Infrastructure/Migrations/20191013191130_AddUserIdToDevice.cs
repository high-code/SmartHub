using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddUserIdToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_measurements",
                table: "measurements");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_devices_DeviceId",
                table: "devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_devices",
                table: "devices");

            migrationBuilder.RenameColumn(
                name: "dtsend",
                table: "measurements",
                newName: "dt_send");

            migrationBuilder.RenameColumn(
                name: "deviceid",
                table: "measurements",
                newName: "device_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "devices",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "devices",
                newName: "device_id");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "devices",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_measurements",
                table: "measurements",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_devices_device_id",
                table: "devices",
                column: "device_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_devices",
                table: "devices",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_measurements",
                table: "measurements");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_devices_device_id",
                table: "devices");

            migrationBuilder.DropPrimaryKey(
                name: "pk_devices",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "devices");

            migrationBuilder.RenameColumn(
                name: "dt_send",
                table: "measurements",
                newName: "dtsend");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "measurements",
                newName: "deviceid");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "devices",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "devices",
                newName: "DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_measurements",
                table: "measurements",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_devices_DeviceId",
                table: "devices",
                column: "DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_devices",
                table: "devices",
                column: "id");
        }
    }
}
