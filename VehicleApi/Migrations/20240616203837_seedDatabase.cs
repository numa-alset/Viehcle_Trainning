﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApi.Migrations
{
    /// <inheritdoc />
    public partial class seedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MAKES (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO MAKES (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',(SELECT Id FROM Makes WHERE Name='Make1'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelB',(SELECT Id FROM Makes WHERE Name='Make1'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelC',(SELECT Id FROM Makes WHERE Name='Make1'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make2-ModelA',(SELECT Id FROM Makes WHERE Name='Make2'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make2-ModelB',(SELECT Id FROM Makes WHERE Name='Make2'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make2-ModelC',(SELECT Id FROM Makes WHERE Name='Make2'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make3-ModelA',(SELECT Id FROM Makes WHERE Name='Make3'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make3-ModelB',(SELECT Id FROM Makes WHERE Name='Make3'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make3-ModelC',(SELECT Id FROM Makes WHERE Name='Make3'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes");
            migrationBuilder.Sql("DELETE FROM Models");

        }
    }
}
