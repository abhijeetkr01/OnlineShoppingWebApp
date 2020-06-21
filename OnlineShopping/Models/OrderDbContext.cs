using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
                : base(options)
        {
        }

        public DbSet<Orders> ordersTb { get; set; }
        public DbSet<OrderDetails> orderDetailsTb { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderDetails>().HasKey(c => new { c.OrderId, c.ProductID });
        }             
    }
}
