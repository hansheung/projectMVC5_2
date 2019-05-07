using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BRO.Models
{
    public class PasswordModel
    {
        public string txtLoginID { get; set; }
        public string txtName { get; set; }
        public string txtPassword { get; set; }
        public string txtConfirm { get; set; }
    }
}