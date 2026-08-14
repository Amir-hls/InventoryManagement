using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public class OrderSummaryDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Guid CustomerId { get; set; }
        //public CustomerDto Customer { get; set; } = new CustomerDto();

        public int OrderStatusId { get; set; }
        public string OrderStatusDisplayName { get; set; } = string.Empty;
    }
}
