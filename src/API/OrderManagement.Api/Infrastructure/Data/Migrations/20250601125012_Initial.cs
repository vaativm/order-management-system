using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagement.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
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

        migrationBuilder.CreateTable(
            name: "OrderStatuses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                State = table.Column<int>(type: "int", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderStatuses_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Orders",
            columns: new[] { "Id", "CreatedAt", "CustomerId", "TotalAmount" },
            values: new object[,]
            {
                { new Guid("1d472ecb-09df-45dd-9c2f-fd552263dfa1"), new DateTime(2025, 6, 1, 12, 49, 59, 210, DateTimeKind.Utc).AddTicks(2461), new Guid("ee8aa160-18e4-4128-bf3b-f2c3008c008c"), 10000m },
                { new Guid("e838ccdc-9c85-43e9-a3a3-cb52bed4d1b0"), new DateTime(2025, 6, 1, 12, 49, 59, 210, DateTimeKind.Utc).AddTicks(2452), new Guid("cc4881a1-8a08-4eb1-8985-be25842484ee"), 5000m }
            });

        migrationBuilder.InsertData(
            table: "Promotions",
            columns: new[] { "Id", "CustomerSegment", "Name", "Type", "ValidFrom", "ValidTo", "Value" },
            values: new object[] { new Guid("7b7bf5b9-3d8b-447f-855c-176fdf9c64fc"), "New", "Vuka", "Percentage", new DateTime(2025, 6, 1, 12, 49, 59, 210, DateTimeKind.Utc).AddTicks(157), null, 10m });

        migrationBuilder.CreateIndex(
            name: "IX_OrderStatuses_OrderId",
            table: "OrderStatuses",
            column: "OrderId",
            unique: true);

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
            name: "OrderStatuses");

        migrationBuilder.DropTable(
            name: "Promotions");

        migrationBuilder.DropTable(
            name: "Orders");
    }
}
