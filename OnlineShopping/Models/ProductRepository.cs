using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductDbContext context;

        public ProductRepository(ProductDbContext context)
        {
            this.context = context;
        }
        public ProductDetails AddProduct(ProductDetails productDetails)
        {
            context.productTb.Add(productDetails);
            context.SaveChanges();
            return productDetails;
        }

        public ProductDetails DeleteProduct(ProductDetails productDetails)
        {
            context.productTb.Remove(productDetails);
            context.SaveChanges();
            return productDetails;
        }

        public ProductDetails GetProduct(string productId)
        {
            return context.productTb.Find(productId);
        }

        public ProductDetails UpdateProduct(ProductDetails productChanges)
        {
            var product = context.productTb.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }
        public IEnumerable<ProductDetails> GetAllUser()
        {
            return context.productTb;
        }
    }
}
