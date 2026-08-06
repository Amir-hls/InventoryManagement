// Infrastructure/Data/Configurations/CustomerConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.TaxNumber).IsUnique();
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.ContactEmail).HasMaxLength(50).IsRequired();
            builder.Property(c => c.CompanyName).HasMaxLength(100);

            // Relationships
            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }


    // Infrastructure/Data/Configurations/ProductConfiguration.cs
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Sku).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.UnitPrice).HasPrecision(10, 2);
        }
    }

    // Infrastructure/Data/Configurations/OrderConfiguration.cs
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.OrderNumber).IsUnique();

            builder.HasOne(o => o.OrderStatus)
                   .WithMany(os => os.Orders)
                   .HasForeignKey(o => o.OrderStatusId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // Infrastructure/Data/Configurations/OrderItemConfiguration.cs
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.UnitPrice).HasPrecision(10, 2);

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Code).HasMaxLength(30).IsRequired();

            builder.HasData(
                new OrderStatus { Id = 1, Code = "PENDING", DisplayName = "Pending Processing" },
                new OrderStatus { Id = 2, Code = "PROCESSING", DisplayName = "In Fulfillment" },
                new OrderStatus { Id = 3, Code = "SHIPPED", DisplayName = "Shipped" },
                new OrderStatus { Id = 4, Code = "DELIVERED", DisplayName = "Delivered" },
                new OrderStatus { Id = 5, Code = "CANCELLED", DisplayName = "Cancelled" }
            );
        }
    }

    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).HasMaxLength(100).IsRequired();
            builder.Property(w => w.Address).HasMaxLength(255);
        }
    }
    public class WarehouseStockConfiguration : IEntityTypeConfiguration<WarehouseStock>
    {
        public void Configure(EntityTypeBuilder<WarehouseStock> builder)
        {
            builder.HasKey(ws => new { ws.WarehouseId, ws.ProductId });
            builder.HasOne(ws => ws.Warehouse).WithMany(w => w.WarehouseStocks)
                .HasForeignKey(ws => ws.WarehouseId);

            builder.HasOne(ws => ws.Product).WithMany(p => p.WarehouseStocks)
                .HasForeignKey(ws => ws.ProductId);
        }
    }
}