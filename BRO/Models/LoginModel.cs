using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BRO.Models
{
    public class LoginModel
    {
        public string txtLOGIN_ID { get; set; }
        public string txtPASSWORD { get; set; }
    }
}