using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BRO.Models
{
    public class LoginModel
    {
        //[Required(ErrorMessage = "Please enter your Login ID")]
        public string txtLoginID { get; set; }
        //[Required(ErrorMessage = "Please enter your Password")]
        public string txtPassword { get; set; }
    }
}