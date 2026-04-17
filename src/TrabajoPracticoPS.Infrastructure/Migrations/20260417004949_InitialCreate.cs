using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrabajoPracticoPS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    RowIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 5, 16, 21, 49, 48, 856, DateTimeKind.Local).AddTicks(6237), "Concierto de Rock", "Active", "Estadio Quilmes" });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "Platea", 5000m },
                    { 2, 50, 1, "Campo", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("01ab23a6-8001-4cf1-a5b4-8785cd5c8c87"), "B", 33, 2, "Available", 1 },
                    { new Guid("0325755e-a2df-41b6-b88e-ce54abf68e58"), "A", 45, 1, "Available", 1 },
                    { new Guid("060e7d9e-c9ec-4ebd-9ae6-7481ba1f33f5"), "A", 23, 1, "Available", 1 },
                    { new Guid("0789abbb-6e71-4ca7-9bf1-e6d230fb706c"), "A", 22, 1, "Available", 1 },
                    { new Guid("1237809a-967c-4c26-9297-a4ede85624a0"), "B", 26, 2, "Available", 1 },
                    { new Guid("126cce98-09b0-4932-bb40-e36074a732a3"), "B", 45, 2, "Available", 1 },
                    { new Guid("1c607730-c4b0-441e-b5f5-8654ad7da1d0"), "B", 47, 2, "Available", 1 },
                    { new Guid("1c861a7c-cc08-4934-8029-086e5e248a78"), "B", 34, 2, "Available", 1 },
                    { new Guid("1d16e681-3ab4-4cb4-a260-da65ea223244"), "B", 5, 2, "Available", 1 },
                    { new Guid("1d7b550e-6d07-4ccc-8a6c-9c3ee38562d3"), "B", 16, 2, "Available", 1 },
                    { new Guid("1fac98d9-0eaa-40e4-aa6e-46f4b20cbdff"), "A", 14, 1, "Available", 1 },
                    { new Guid("24fd44ec-909b-46df-88ae-591649aa4a0f"), "B", 42, 2, "Available", 1 },
                    { new Guid("28896089-a43b-43de-8e7e-717de6e322eb"), "A", 49, 1, "Available", 1 },
                    { new Guid("2bbb4aa3-87ac-4398-94d8-15c5c78c6631"), "B", 24, 2, "Available", 1 },
                    { new Guid("2c94dacc-8af2-43c0-8e4d-972f51c83a12"), "B", 38, 2, "Available", 1 },
                    { new Guid("2d2ba38e-5403-451a-a051-6a31d91f7d0f"), "B", 9, 2, "Available", 1 },
                    { new Guid("2dbb105a-fafd-4032-bed2-9db729ba56b5"), "A", 7, 1, "Available", 1 },
                    { new Guid("315a7252-a167-413b-9385-df17700f96cb"), "A", 48, 1, "Available", 1 },
                    { new Guid("31b670c3-9634-4eb6-8f7a-769ca088d972"), "A", 40, 1, "Available", 1 },
                    { new Guid("332a6592-7741-4a80-82bc-2b49b261d22f"), "A", 15, 1, "Available", 1 },
                    { new Guid("33d10dea-5c11-4585-bd79-3847ec80a1dd"), "A", 5, 1, "Available", 1 },
                    { new Guid("3840b521-f7e2-4b23-b6e2-fce09e309879"), "A", 35, 1, "Available", 1 },
                    { new Guid("39ad226d-a2b1-460a-965c-845186c42665"), "B", 49, 2, "Available", 1 },
                    { new Guid("3a3cec7b-b789-4dd9-bb72-4c3834662de0"), "B", 28, 2, "Available", 1 },
                    { new Guid("3c038679-2375-44d2-b0c5-0947d34b2808"), "A", 38, 1, "Available", 1 },
                    { new Guid("40866d64-4c80-46be-9bf0-03c400fa19a9"), "A", 2, 1, "Available", 1 },
                    { new Guid("40bd93b0-b4f3-405f-93af-378591db7204"), "A", 42, 1, "Available", 1 },
                    { new Guid("4114ad5c-0c06-4405-bea6-af430b39a4ad"), "A", 50, 1, "Available", 1 },
                    { new Guid("4cf41eca-81fa-48a9-8628-2823ca124034"), "B", 20, 2, "Available", 1 },
                    { new Guid("4d4248ca-ec0b-4e18-b6e5-63c5e13a28fb"), "A", 30, 1, "Available", 1 },
                    { new Guid("5348cc15-0442-4f46-9eb9-ace97a6efcc2"), "A", 34, 1, "Available", 1 },
                    { new Guid("54927796-15d6-4c3c-a028-f3718492f68e"), "A", 33, 1, "Available", 1 },
                    { new Guid("55de8527-1fe1-40b5-9f14-a3d6e51951fe"), "A", 17, 1, "Available", 1 },
                    { new Guid("59a78eab-46de-496a-90a6-72dea44ce6e2"), "A", 9, 1, "Available", 1 },
                    { new Guid("5a5c9e42-92f8-4195-8048-527b4e6e17e4"), "A", 19, 1, "Available", 1 },
                    { new Guid("5bec7dba-2dcb-4241-b2dc-86df1ad05cfa"), "B", 21, 2, "Available", 1 },
                    { new Guid("5c553356-f963-4414-952b-1be5bced849a"), "B", 1, 2, "Available", 1 },
                    { new Guid("5c7a5611-d89a-485c-9a77-3faae3469fa3"), "B", 11, 2, "Available", 1 },
                    { new Guid("5d92f126-ad8a-4551-8f7b-f37cdf8464c5"), "B", 18, 2, "Available", 1 },
                    { new Guid("5db7f692-9527-42c4-9a25-d486811ff11b"), "B", 31, 2, "Available", 1 },
                    { new Guid("5dda1fb4-bea9-4af6-83b7-8d51d93c2a39"), "A", 44, 1, "Available", 1 },
                    { new Guid("5e822932-f7f3-40ed-8f52-2d82272fc905"), "A", 18, 1, "Available", 1 },
                    { new Guid("5f519dd3-7aa7-424e-ace0-b3a83f7407d7"), "B", 2, 2, "Available", 1 },
                    { new Guid("5fdf21b5-f6ef-4077-aad2-1512fbc1b015"), "A", 31, 1, "Available", 1 },
                    { new Guid("6062b585-bfd1-4b9a-8075-89b0bbd82991"), "A", 16, 1, "Available", 1 },
                    { new Guid("684d4421-f436-4fef-8470-33f051121873"), "B", 43, 2, "Available", 1 },
                    { new Guid("68f03f46-5581-44b8-8590-a64eca38093b"), "B", 23, 2, "Available", 1 },
                    { new Guid("69a7bb89-379e-4c6a-b0a4-cec088e57378"), "A", 46, 1, "Available", 1 },
                    { new Guid("6d0ea1d7-92a6-4518-9bdc-90040c6e7023"), "B", 29, 2, "Available", 1 },
                    { new Guid("71680c5f-21ce-4d30-9f75-9cd314a212bb"), "B", 19, 2, "Available", 1 },
                    { new Guid("7215513b-f7d1-438a-9672-e66e9e2c9124"), "B", 17, 2, "Available", 1 },
                    { new Guid("727a26a1-ef3c-4b8b-b2f5-466c3d40ad4b"), "B", 32, 2, "Available", 1 },
                    { new Guid("7f2aab62-92ff-4d0f-a6c8-11df955fa0dc"), "B", 3, 2, "Available", 1 },
                    { new Guid("7faeb655-24e5-40fe-92bc-d9d6f025b5aa"), "B", 10, 2, "Available", 1 },
                    { new Guid("813ccb9d-0edf-484c-b211-dde9224d9c0d"), "B", 40, 2, "Available", 1 },
                    { new Guid("87f15c0c-02c0-49b3-9249-1eeeadc07ce4"), "A", 32, 1, "Available", 1 },
                    { new Guid("8a225f37-4c8a-4edd-b4bf-26e7a15ffa01"), "B", 37, 2, "Available", 1 },
                    { new Guid("8d08903c-831d-453f-8df8-831dd94a0eda"), "A", 24, 1, "Available", 1 },
                    { new Guid("8de30b19-874e-4932-9bb6-689adad69f7b"), "B", 48, 2, "Available", 1 },
                    { new Guid("8fe28758-b287-4175-adfa-5a5a2e2edbb3"), "A", 36, 1, "Available", 1 },
                    { new Guid("a1ffa1b2-143c-4a09-bb90-61312b700382"), "A", 43, 1, "Available", 1 },
                    { new Guid("a4d12652-7964-4134-a62d-72bb8fb1a010"), "A", 41, 1, "Available", 1 },
                    { new Guid("a68026ab-7e8c-4c90-b489-64c873579abc"), "B", 6, 2, "Available", 1 },
                    { new Guid("a8716952-4c9d-4a1f-be04-8336356b8ff7"), "A", 3, 1, "Available", 1 },
                    { new Guid("a9472741-1751-4be7-b7c3-4eaf43c7f2c6"), "A", 39, 1, "Available", 1 },
                    { new Guid("abbb365c-3687-478b-845c-1365ed5138b0"), "A", 11, 1, "Available", 1 },
                    { new Guid("abe1d080-1c3d-4c1f-8efa-531a5c877a91"), "B", 50, 2, "Available", 1 },
                    { new Guid("abf36332-8116-4d04-a973-68b6bd26a6b7"), "A", 1, 1, "Available", 1 },
                    { new Guid("ae74bc20-8bc4-47bf-8b4e-883f7b875014"), "B", 30, 2, "Available", 1 },
                    { new Guid("aec88b87-d249-430b-899e-d4b9bcb8db51"), "A", 20, 1, "Available", 1 },
                    { new Guid("af534dca-8465-4f78-9fea-490753deee4c"), "B", 22, 2, "Available", 1 },
                    { new Guid("af9bd8d7-4d45-4b38-89a8-da2bc14ad14e"), "B", 12, 2, "Available", 1 },
                    { new Guid("b0b7689c-0060-49c0-b8af-4fad9c76ab59"), "B", 4, 2, "Available", 1 },
                    { new Guid("b4ca70bc-1346-49b2-a950-6fb0469397a7"), "B", 8, 2, "Available", 1 },
                    { new Guid("b8931661-32ae-46b5-9758-a6ccce9a11c4"), "A", 13, 1, "Available", 1 },
                    { new Guid("b8a4493b-1be1-41b9-92be-cbf362a2abd6"), "A", 6, 1, "Available", 1 },
                    { new Guid("b9c4fc6f-bd51-4e73-8326-e0e4c01d4631"), "A", 21, 1, "Available", 1 },
                    { new Guid("bba29e14-add4-436b-9569-8195ef4fec68"), "A", 29, 1, "Available", 1 },
                    { new Guid("bf45522e-b493-4b88-abf0-176f068afd34"), "A", 4, 1, "Available", 1 },
                    { new Guid("c69e6bbf-4965-4b65-aa1d-a39b35206b15"), "B", 25, 2, "Available", 1 },
                    { new Guid("c6b58ad3-7f7d-417f-a9c0-4aacccfeabae"), "A", 12, 1, "Available", 1 },
                    { new Guid("c712db32-01f0-4ea7-82a6-2e09ef0883e3"), "B", 35, 2, "Available", 1 },
                    { new Guid("c7be15ed-6eb5-4da7-bcb6-adf5d367a335"), "B", 44, 2, "Available", 1 },
                    { new Guid("c8cb6623-f4e4-4a9c-a49a-29804ec60ca6"), "B", 46, 2, "Available", 1 },
                    { new Guid("c8cbd8d7-f589-4091-a7ca-5260e5c06a4e"), "B", 15, 2, "Available", 1 },
                    { new Guid("cdd73eae-b9e1-436e-8673-e54743457ce3"), "B", 36, 2, "Available", 1 },
                    { new Guid("ce7e7b13-2fa7-4c50-834e-dcc2b623f74d"), "B", 39, 2, "Available", 1 },
                    { new Guid("d0f53b7f-97f2-4438-8526-669939ea27e0"), "A", 8, 1, "Available", 1 },
                    { new Guid("d58b7247-3b15-415c-936d-a841e5676b61"), "A", 37, 1, "Available", 1 },
                    { new Guid("e34f8120-2e11-4df1-b725-cb1268d1ff4f"), "B", 27, 2, "Available", 1 },
                    { new Guid("e77a3e80-e576-40d7-a055-36e5abe086a1"), "B", 13, 2, "Available", 1 },
                    { new Guid("e92887ad-3e1e-4f16-b6ff-9ad22996f276"), "A", 27, 1, "Available", 1 },
                    { new Guid("ecaad3ff-f14c-45a8-b2b1-e554df0abb4b"), "A", 47, 1, "Available", 1 },
                    { new Guid("f126ec2c-b55c-4309-a544-71398a6e00ba"), "A", 25, 1, "Available", 1 },
                    { new Guid("f217e74f-e4a0-42be-9b5f-2da0e90f6a8c"), "A", 26, 1, "Available", 1 },
                    { new Guid("f266b081-2c88-40cf-ba3f-3ac9fc6b9510"), "B", 14, 2, "Available", 1 },
                    { new Guid("f2da0268-76af-4e50-be97-eb3e833248bf"), "A", 28, 1, "Available", 1 },
                    { new Guid("f5f8b858-ac32-4fa1-a09f-230b3edf5b7c"), "B", 7, 2, "Available", 1 },
                    { new Guid("f7595acc-b0c8-4237-a1c1-ad548bb8c978"), "B", 41, 2, "Available", 1 },
                    { new Guid("fc7fc4c4-15ed-45b0-bc06-88bcb563a845"), "A", 10, 1, "Available", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatId",
                table: "Reservations",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectorId",
                table: "Seats",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_EventId",
                table: "Sectors",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
