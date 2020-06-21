using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductsViewModel 
    {
        [Required]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage ="ProductID must be integer.")]
        public string ProductID { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }
        [RegularExpression(@"^(0\.\d*)?[1-9]\d*(\.\d*)?$", ErrorMessage = "Unit Price must be valid price")]
        public string UnitPrice { get; set; }
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter valid Quantity.")]
        public string Quantity { get; set; }
        public IFormFile Photo { get; set; }
        public string ExistingPhotoPath { get; set; }

       
    }
}
