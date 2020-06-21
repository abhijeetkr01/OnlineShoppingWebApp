using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext context;

        public UserRepository(UserDbContext context)
        {
            this.context = context;
        }

        public UserDetails AddUser(UserDetails userDetails)
        {
            context.userTb.Add(userDetails);
            context.SaveChanges();
            return userDetails;
        }

        public UserDetails GetUser(string userId)
        {
            return context.userTb.Find(userId);
        }
    }
}
