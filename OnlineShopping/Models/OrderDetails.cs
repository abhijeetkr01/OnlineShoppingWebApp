using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class OrderDetails 
    {
        
        [ForeignKey("Orders")]
        [Key]
        public string OrderId { get; set; }
        public string ProductID { get; set; }
        public string RequiredQunatity { get; set; }
        [Required]
        public string Amount { get; set; }
        public Orders Orders { get; set; }
    }
}
