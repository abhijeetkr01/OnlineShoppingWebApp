using System;
using OnlineShopping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }       
        public string Pin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string LanguagesKnown { get; set; }
    }
}
