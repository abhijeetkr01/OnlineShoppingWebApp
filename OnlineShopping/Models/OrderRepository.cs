using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext context;
       

        public OrderRepository(OrderDbContext context)
        {
            this.context = context;
        }



        public OrderDetails AddorderDetails(OrderDetails orderDetails)
        {
            context.orderDetailsTb.Add(orderDetails);
            context.SaveChanges();
            return orderDetails;
        }

        public Orders AddOrders(Orders orders)
        {
            context.ordersTb.Add(orders);
            context.SaveChanges();
            return orders;
        }

       
    }
}
