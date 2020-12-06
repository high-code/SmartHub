using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Edge.Infrastructure.Migrations
{
  public partial class RenameAndDeleteDeviceIdOldField : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {

      migrationBuilder.RenameColumn(name:"device_id", 
                                    table: "measurements", 
                                    newName: "device_id_old");

      migrationBuilder.AddColumn<Guid>(
          name: "device_id",
          table: "measurements",
          nullable: false,
          defaultValueSql: "uuid_generate_v1()");

      migrationBuilder.AddColumn<DateTime>(
          name: "dt_received",
          table: "measurements",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.DropColumn(name:"device_id_old", 
                                  table: "measurements");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "dt_received",
          table: "measurements");

      migrationBuilder.DropColumn(
        name: "device_id",
        table: "measurements");

      migrationBuilder.AddColumn<int>(
          name: "device_id",
          table: "measurements",
          nullable: false);
    }
  }
}
