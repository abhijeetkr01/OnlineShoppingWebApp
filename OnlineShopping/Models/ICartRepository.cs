using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public interface ICartRepository
    {
        CartDetails AddtoCart(CartDetails orderDetails);

        CartDetails GetOrderDetails(string productId);

        IList<CartDetails> GetAllOrder();

        IList<CartDetails> ClearCart();
    }
}
