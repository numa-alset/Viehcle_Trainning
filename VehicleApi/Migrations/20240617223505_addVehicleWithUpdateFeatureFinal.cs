using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApi.Migrations
{
    /// <inheritdoc />
    public partial class addVehicleWithUpdateFeatureFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureVehicle");

            migrationBuilder.CreateTable(
                name: "FeatureVehicles",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureVehicles", x => new { x.FeatureId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_FeatureVehicles_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureVehicles_VehicleId",
                table: "FeatureVehicles",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureVehicles");

            migrationBuilder.CreateTable(
                name: "FeatureVehicle",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureVehicle", x => new { x.FeaturesId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_FeatureVehicle_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureVehicle_VehiclesId",
                table: "FeatureVehicle",
                column: "VehiclesId");
        }
    }
}
