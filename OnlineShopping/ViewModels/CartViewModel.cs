﻿using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class CartViewModel
    {
       public IList<CartDetails> order { get; set; }
    }
}
