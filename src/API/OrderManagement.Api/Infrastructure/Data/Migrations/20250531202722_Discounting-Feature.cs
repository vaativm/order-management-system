using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagement.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class DiscountingFeature : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Orders", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Promotions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                CustomerSegment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Promotions", x => x.Id));

        migrationBuilder.InsertData(
            table: "Orders",
            columns: new[] { "Id", "CreatedAt", "CustomerId", "Status", "TotalAmount" },
            values: new object[,]
            {
                { new Guid("89b166d4-ac78-4c21-97c2-8944eaccdb43"), new DateTime(2025, 5, 31, 20, 27, 8, 118, DateTimeKind.Utc).AddTicks(2496), new Guid("523bca92-5f31-438c-a877-f2909837c2b8"), 2, 10000m },
                { new Guid("d94d8bdf-ffcf-4f09-865a-fc3444510aa7"), new DateTime(2025, 5, 31, 20, 27, 8, 118, DateTimeKind.Utc).AddTicks(2401), new Guid("c6326406-e65f-44d1-82e3-880c09c823ec"), 0, 5000m }
            });

        migrationBuilder.InsertData(
            table: "Promotions",
            columns: new[] { "Id", "CustomerSegment", "Name", "Type", "ValidFrom", "ValidTo", "Value" },
            values: new object[] { new Guid("0aec6cd5-ace9-490b-ba10-33fe85cc5aae"), "New", "Vuka", "Percentage", new DateTime(2025, 5, 31, 20, 27, 8, 117, DateTimeKind.Utc).AddTicks(9307), null, 10m });

        migrationBuilder.CreateIndex(
            name: "IX_Promotions_CustomerSegment",
            table: "Promotions",
            column: "CustomerSegment");

        migrationBuilder.CreateIndex(
            name: "IX_Promotions_ValidFrom",
            table: "Promotions",
            column: "ValidFrom");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Promotions");
    }
}
