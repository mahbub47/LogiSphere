using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiSphere.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class TenantInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlateNumber = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    MaxWeightCapacityKg = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRoutes_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    SequenceOrder = table.Column<int>(type: "integer", nullable: false),
                    DestinationAddress = table.Column<string>(type: "text", nullable: false),
                    CargoWeightKg = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryStops_DeliveryRoutes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRoutes_VehicleId",
                table: "DeliveryRoutes",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStops_RouteId",
                table: "DeliveryStops",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryStops");

            migrationBuilder.DropTable(
                name: "DeliveryRoutes");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
