using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } 
        public int MaxCapacity { get; set; }
        public ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();


    }
}
