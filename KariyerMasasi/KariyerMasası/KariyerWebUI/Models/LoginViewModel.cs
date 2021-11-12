using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KariyerWebUI.Models
{
    public class LoginViewModel
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}