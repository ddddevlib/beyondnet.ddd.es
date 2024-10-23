using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DddEsSample");

            migrationBuilder.CreateTable(
                name: "aggregaterootsample",
                schema: "DddEsSample",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityRef1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityRef2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSpan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aggregaterootsample", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventdataerrors",
                schema: "DddEsSample",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventdataerrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventdatarecords",
                schema: "DddEsSample",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    AssemblyQualifyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventdatarecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aggregaterootsample",
                schema: "DddEsSample");

            migrationBuilder.DropTable(
                name: "eventdataerrors",
                schema: "DddEsSample");

            migrationBuilder.DropTable(
                name: "eventdatarecords",
                schema: "DddEsSample");
        }
    }
}
