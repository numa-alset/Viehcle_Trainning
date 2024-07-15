﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApi.Migrations
{
    /// <inheritdoc />
    public partial class addVehicleWithUpdateFeature3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "Vehicles");
        }
    }
}
