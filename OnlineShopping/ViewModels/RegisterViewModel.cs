using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Controllers;
using OnlineShopping.CustomValidate;
using OnlineShopping.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="User Type")]
        public UserTypeEnum? UserType { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-z]{1}.*\d*$",ErrorMessage ="Password must start with an alphabet.")]
        [MaxLength(15,ErrorMessage ="Password cannot be more than 15 characters.")]
        public string Password { get; set; }
        [MaxLength(10)]
        public string UserID { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^\S(.*\S)?$", ErrorMessage ="Address must not start and end with sapce.")]
        public string Address { get; set; }
        [Required]
        public UserCountryEnum? Country { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Pin must of 6 digits only.")]
        [MaxLength(6)]
        public string Pin { get; set; }
        [Required]
        [ValidEmailDomain(ErrorMessage ="Enter valid Email Domain.")]
        [EmailAddress]
        [MaxLength(30, ErrorMessage ="Email must be only 30 characters long.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\+[0-9]{2}-[0-9]{10}$", ErrorMessage = "Mobile Number should be of format +**-*********.")]
        public string Phone { get; set; }
        [Required]
        public UserGenderEnum Gender { get; set; }
        [Display(Name="Languages Known")]
        public string LanguagesKnown { get; set; }
    }

  
}
