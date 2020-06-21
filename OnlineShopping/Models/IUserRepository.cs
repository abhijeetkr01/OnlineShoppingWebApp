using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public interface IUserRepository
    {
        UserDetails AddUser(UserDetails userDetails);

        UserDetails GetUser(string userId);


    }
}
