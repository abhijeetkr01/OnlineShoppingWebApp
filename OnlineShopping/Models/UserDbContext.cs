using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class UserDbContext : IdentityDbContext<UserTypeClass>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            :base(options)
        { 
        }

        public DbSet<UserDetails> userTb { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
