using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportEvents_Sales_Front_End.Model
{
    public class AuthModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}