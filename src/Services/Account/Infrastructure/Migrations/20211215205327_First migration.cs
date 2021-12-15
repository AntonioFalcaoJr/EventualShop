using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountSnapshots",
                columns: table => new
                {
                    AggregateVersion = table.Column<int>(type: "int", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    AggregateState = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSnapshots", x => new { x.AggregateVersion, x.AggregateId });
                });

            migrationBuilder.CreateTable(
                name: "AccountStoreEvents",
                columns: table => new
                {
                    Version = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    EventName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStoreEvents", x => x.Version);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSnapshots");

            migrationBuilder.DropTable(
                name: "AccountStoreEvents");
        }
    }
}
