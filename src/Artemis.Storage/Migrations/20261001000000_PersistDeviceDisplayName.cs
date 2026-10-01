using Artemis.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artemis.Storage.Migrations;

[DbContext(typeof(ArtemisDbContext))]
[Migration("20261001000000_PersistDeviceDisplayName")]
public partial class PersistDeviceDisplayName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "DisplayName",
            table: "Devices",
            type: "TEXT",
            maxLength: 512,
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "DisplayName", table: "Devices");
    }
}
