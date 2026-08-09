using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. Define Static Guids so Foreign Keys can link correctly
            var customerId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var customerId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var productId1 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var productId2 = Guid.Parse("44444444-4444-4444-4444-444444444444");

            var warehouseId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var orderId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            // Pro-Tip: Use a static DateTime for seeded data so EF Core doesn't 
            // generate a new migration every time the clock ticks.
            var seedDate = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc);

            // 2. Seed Lookups (Integers)
            //modelBuilder.Entity<OrderStatus>().HasData(
            //    new OrderStatus { Id = 1, Code = "PENDING", DisplayName = "Pending Approval" },
            //    new OrderStatus { Id = 2, Code = "PROCESSING", DisplayName = "Processing in Warehouse" },
            //    new OrderStatus { Id = 3, Code = "SHIPPED", DisplayName = "Shipped to Customer" }
            //);

            // 3. Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = customerId1,
                    Name = "Alice Tech",
                    PhoneNumber = "+31612345678",
                    CompanyName = "Tech Solutions B.V.",
                    ContactEmail = "alice@techsolutions.nl",
                    TaxNumber = "NL812345678B01",
                    CreatedAt = seedDate
                },
                new Customer
                {
                    Id = customerId2,
                    Name = "Bob Logistics",
                    PhoneNumber = "+31687654321",
                    CompanyName = "Fast Freight GmbH",
                    ContactEmail = "bob@fastfreight.de",
                    TaxNumber = "DE123456789",
                    CreatedAt = seedDate
                }
            );

            // 4. Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = productId1, Sku = "CPU-INT-i9", Name = "Intel Core i9-14900K", UnitPrice = 599.99m },
                new Product { Id = productId2, Sku = "GPU-NV-4090", Name = "NVIDIA RTX 4090", UnitPrice = 1599.99m }
            );

            // 5. Seed Warehouse
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse
                {
                    Id = warehouseId,
                    Name = "Central Europe Hub",
                    Address = "123 Logistics Way, Amsterdam",
                    MaxCapacity = 10000
                }
            );

            // 6. Seed Warehouse Stock (Linking Product & Warehouse)
            modelBuilder.Entity<WarehouseStock>().HasData(
                new WarehouseStock { Id = 1, WarehouseId = warehouseId, ProductId = productId1, StockQuantity = 150 },
                new WarehouseStock { Id = 2, WarehouseId = warehouseId, ProductId = productId2, StockQuantity = 45 }
            );

            // 7. Seed Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = orderId,
                    OrderNumber = "ORD-2026-001",
                    OrderDate = seedDate,
                    CustomerId = customerId1,  // Linked to Alice Tech
                    OrderStatusId = 1          // Linked to PENDING
                }
            );

            // 8. Seed Order Items (Linking Order & Products)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    OrderId = orderId,
                    ProductId = productId1,
                    UnitPrice = 599.99m, // Historical price at time of order
                    Quantity = 5
                },
                new OrderItem
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    OrderId = orderId,
                    ProductId = productId2,
                    UnitPrice = 1599.99m,
                    Quantity = 2
                }
            );
        }
    }
}