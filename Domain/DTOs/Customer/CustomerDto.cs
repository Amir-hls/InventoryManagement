using Domain.DTOs.Order;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public List<CustomerOrderDto> Orders { get; set; } = new();
    }
}
