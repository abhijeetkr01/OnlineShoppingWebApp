using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.CustomValidate
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value.ToString();
            int index = email.LastIndexOf('.');
            string domain = email.Substring(index + 1).ToLower();
            if (domain == "in" || domain == "co" || domain == "edu" || domain == "gov" || domain == "info" || domain == "net" || domain == "org")
                return true;
            else
                return false;
        }
    }
}
