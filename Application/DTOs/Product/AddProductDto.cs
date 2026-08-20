using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Product
{
    public class AddProductDto
    {
        [Required]
        public string Sku { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}
