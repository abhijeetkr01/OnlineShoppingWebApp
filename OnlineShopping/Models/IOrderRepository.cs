using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public interface IOrderRepository
    {
        

        Orders AddOrders(Orders orders);

        OrderDetails AddorderDetails(OrderDetails orderDetails);

    }
}
