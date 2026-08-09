using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WarehouseStock
    {
        public int Id { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int StockQuantity { get; set; }
    }
}
