using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class CartRepository : ICartRepository
    {
        private List<CartDetails> _orderList;

        public CartRepository()
        {
            _orderList = new List<CartDetails>();
            
        }
        public CartDetails AddtoCart(CartDetails order)
        {
            _orderList.Add(order);
            return order;

        }

        public IList<CartDetails> GetAllOrder()
        {
            return _orderList;
        }

        public IList<CartDetails> ClearCart()
        {
            _orderList.Clear();
            return _orderList;
        }

        public CartDetails GetOrderDetails(string productId)
        {
            return _orderList.Find(x => x.ProductID == productId);
        }
    }
}
