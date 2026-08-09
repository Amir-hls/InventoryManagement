using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        // Historical price snapshot at the moment of order placement
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
