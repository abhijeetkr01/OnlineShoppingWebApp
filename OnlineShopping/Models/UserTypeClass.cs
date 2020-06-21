using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class UserTypeClass : IdentityUser
    {
        [Required]
        public char UserType { get; set; }
    }
}
