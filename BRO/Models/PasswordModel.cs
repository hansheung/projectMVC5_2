using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BRO.Models
{
    public class PasswordModel
    {
        public string txtLOGIN_ID { get; set; }
        public string txtNAME { get; set; }
        public string txtPASSWORD { get; set; }
        public string txtCONFIRM { get; set; }
    }
}