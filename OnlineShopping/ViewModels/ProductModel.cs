using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductModel
    {
        public IEnumerable<ProductDetails> AllProducts { get; set; }
        
        public ProductsViewModel Product { get; set; }

    }
}
