using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CompanyName", "ContactEmail", "CreatedAt", "Name", "PhoneNumber", "TaxNumber" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Tech Solutions B.V.", "alice@techsolutions.nl", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alice Tech", "+31612345678", "NL812345678B01" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Fast Freight GmbH", "bob@fastfreight.de", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bob Logistics", "+31687654321", "DE123456789" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Sku", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Intel Core i9-14900K", "CPU-INT-i9", 599.99m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "NVIDIA RTX 4090", "GPU-NV-4090", 1599.99m }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "Address", "MaxCapacity", "Name" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "123 Logistics Way, Amsterdam", 10000, "Central Europe Hub" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderNumber", "OrderStatusId" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-2026-001", 1 });

            migrationBuilder.InsertData(
                table: "WarehouseStock",
                columns: new[] { "Id", "ProductId", "StockQuantity", "WarehouseId" },
                values: new object[,]
                {
                    { 1, new Guid("33333333-3333-3333-3333-333333333333"), 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { 2, new Guid("44444444-4444-4444-4444-444444444444"), 45, new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333"), 5, 599.99m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("44444444-4444-4444-4444-444444444444"), 2, 1599.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "WarehouseStock",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WarehouseStock",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
