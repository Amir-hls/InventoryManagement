using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // e.g., "PENDING", "PROCESSING", "SHIPPED"
        public string DisplayName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
