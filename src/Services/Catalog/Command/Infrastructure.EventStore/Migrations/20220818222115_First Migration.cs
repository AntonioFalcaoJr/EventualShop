using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EventStore.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    DomainEventName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DomainEvent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => new { x.Version, x.AggregateId });
                });

            migrationBuilder.CreateTable(
                name: "Snapshots",
                columns: table => new
                {
                    AggregateVersion = table.Column<long>(type: "bigint", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Aggregate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshots", x => new { x.AggregateVersion, x.AggregateId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Snapshots");
        }
    }
}
