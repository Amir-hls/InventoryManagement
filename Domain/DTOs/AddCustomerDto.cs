using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class AddCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? CompanyName { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MaxLength(20)]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TaxNumber { get; set; }
    }
}
