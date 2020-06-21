using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class ProductDetails
    {
        [Required]
        [MaxLength(10)]
        [Key]
        public string ProductID { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Please enter valid Quantity.")]
        public string Quantity { get; set; }
        public string PhotoPath { get; set; }
    }
}
