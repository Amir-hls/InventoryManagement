using Domain.DTOs.Customer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Order
{
    public class CustomerOrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Guid CustomerId { get; set; }
        //public CustomerDto Customer { get; set; } = new CustomerDto();

        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; } = null!;
    }
}
