using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class UserDetails
    {
        [Required]
        [MaxLength(10)]
        [Key]
        public string UserID { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [MaxLength(6)]
        public string Pin { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string LanguagesKnown { get; set; }
    }
}
