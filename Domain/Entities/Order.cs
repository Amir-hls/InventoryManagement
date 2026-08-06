using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; } = null!;

        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
