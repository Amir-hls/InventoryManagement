using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.WarehouseStock
{
    public class StockDto
    {
        public int StockQuantity { get; set; }
        public string? WarehouseName { get; set;  }
        public string? WarehouseAddress { get; set; }
    }
}
