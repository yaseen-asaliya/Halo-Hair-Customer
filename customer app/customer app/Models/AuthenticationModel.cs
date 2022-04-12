using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ulr { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string location { get; set; }
    }
}
