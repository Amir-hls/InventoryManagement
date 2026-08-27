using Application.DTOs.WarehouseStock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public List<StockDto>? StockDtos { get; set; }



    }
}
