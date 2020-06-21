using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    
    public class Orders
    {
        [Required]
        [Key]
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string TotalQunatity { get; set; }
        [Required]
        public string OrderAmount { get; set; }

    }
}
