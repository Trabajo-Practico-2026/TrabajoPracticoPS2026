using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrabajoPracticoPS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SECTOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECTOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SECTOR_EVENT_EventId",
                        column: x => x.EventId,
                        principalTable: "EVENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUDIT_LOG",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_LOG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUDIT_LOG_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SEAT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    RowIdentifier = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEAT_SECTOR_SectorId",
                        column: x => x.SectorId,
                        principalTable: "SECTOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESERVATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATION_SEAT_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SEAT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESERVATION_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EVENT",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 6, 1, 21, 0, 0, 0, DateTimeKind.Unspecified), "Concierto de Rock", "Active", "Estadio Quilmes" });

            migrationBuilder.InsertData(
                table: "SECTOR",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "Platea", 5000m },
                    { 2, 50, 1, "Campo", 3000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUDIT_LOG_UserId",
                table: "AUDIT_LOG",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_SeatId",
                table: "RESERVATION",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_UserId",
                table: "RESERVATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SEAT_SectorId",
                table: "SEAT",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SECTOR_EventId",
                table: "SECTOR",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_Email",
                table: "USER",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDIT_LOG");

            migrationBuilder.DropTable(
                name: "RESERVATION");

            migrationBuilder.DropTable(
                name: "SEAT");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SECTOR");

            migrationBuilder.DropTable(
                name: "EVENT");
        }
    }
}
