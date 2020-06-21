using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductDetails> GetAllUser();
        ProductDetails AddProduct(ProductDetails productDetails);
        ProductDetails GetProduct(string productId);

        ProductDetails UpdateProduct(ProductDetails productDetails);

        ProductDetails DeleteProduct(ProductDetails productDetails);
    }
}
